using UnityEngine;

namespace ShmupBoss
{
    [AddComponentMenu("Shmup Boss/Agents/Pickup")]
    public class Pickup : MonoBehaviour
    {
        [Tooltip("How many clones of this item will be pooled. Try to use a number close to the maximum you " +
            "believe will ever be used on screen.")]
        [SerializeField]
        private int poolLimit;
        public int PoolLimit
        {
            get
            {
                return poolLimit;
            }
        }

        [Tooltip("When adding multiple pickup effects, please pay careful attention to the order of your effects. " +
            "For example always place a health upgrade BEFORE a health fill or heal and not the other way round.")]
        [SerializeField]
        private PickupEffect[] pickupEffects;

        private SfxDrops sfxDrops;

        private void Awake()
        {
            // Get the SfxDrops component if it exists on the same GameObject
            sfxDrops = GetComponent<SfxDrops>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            HitByPlayer(collision);
        }

        private void HitByPlayer(Collider2D collision)
        {
            if (collision.gameObject.layer == ProjectLayers.Player)
            {
                Player player = collision.GetComponent<Player>();

                if (player == null)
                {
                    return;
                }

                for (int i = 0; i < pickupEffects.Length; i++)
                {
                    PickupType pickupType = pickupEffects[i].Type;
                    int amount = pickupEffects[i].Amount;

                    switch (pickupType)
                    {
                        case PickupType.Heal:
                            player.Heal(amount);
                            break;

                        case PickupType.Health:
                            player.FillHealth(amount);
                            break;

                        case PickupType.Shield:
                            player.FillShield(amount);
                            break;

                        case PickupType.HealthUpgrade:
                            player.UpgradeHealth(amount);
                            break;

                        case PickupType.ShieldUpgrade:
                            player.UpgradeShield(amount);
                            break;

                        case PickupType.Invincibilty:
                            player.AddImmunityTime(amount);
                            break;

                        case PickupType.Lives:
                            if (Level.Instance != null)
                            {
                                Level.Instance.AdjustPlayerLives(amount);
                            }

                            player.NotifyOfPlayerLivesPickup();
                            break;

                        case PickupType.WeaponUpgrade:
                            player.UpgradeWeapons(amount);
                            break;

                        case PickupType.VisualUpgrade:
                            player.UpgradeVisual(amount);
                            break;

                        case PickupType.SpeedChange:
                            player.AdjustSpeed(amount);
                            break;

                        case PickupType.Point:
                            if (Level.Instance != null)
                            {
                                Level.Instance.AddScore(amount);
                            }

                            player.NotifiyOfPointsPickup();
                            break;

                        case PickupType.Coin:
                            if (Level.Instance != null)
                            {
                                Level.Instance.AddCoins(amount);
                            }

                            player.NotifiyOfCoinsPickup();
                            break;

                        case PickupType.Bomb:
                            if (Bomb.Instance != null)
                            {
                                Bomb.Instance.AdjustBombsCount(amount);
                            }

                            player.NotifiyOfBombsPickup();
                            break;

                        default:
                            Debug.Log("Pickup.cs: Can't find an implementation for this type of pickup: " + pickupType);
                            break;
                    }

                    player.NotifiyOfPickup(pickupType);
                }

                // Play the pickup sound before despawning
                if (sfxDrops != null)
                {
                    sfxDrops.PlayPickupSound();
                }

                if (PickupPool.Instance != null)
                {
                    PickupPool.Instance.Despawn(gameObject);
                }
            }
        }
    }
}
