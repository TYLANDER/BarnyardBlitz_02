using UnityEngine;

namespace ShmupBoss
{
    /// <summary>
    /// Component for playing sound effects when a drop (powerup) is picked up.
    /// </summary>
    [AddComponentMenu("Shmup Boss/FX/Drop/SFX Drops")]
    public class SfxDrops : MonoBehaviour
    {
        /// <summary>
        /// The audio clip that will play when the drop is picked up.
        /// </summary>
        [Tooltip("The audio clip to play when this drop is picked up.")]
        public AudioClip pickupSound;

        /// <summary>
        /// The volume at which the audio clip will be played.
        /// </summary>
        [Tooltip("Volume of the pickup sound.")]
        [Range(0f, 1f)]
        public float volume = 1.0f;

        /// <summary>
        /// Plays the pickup sound using the SfxSource to manage audio sources.
        /// </summary>
        public void PlayPickupSound()
        {
            if (pickupSound != null)
            {
                SfxSource.Instance.PlayClip(pickupSound, volume);
            }
            else
            {
                Debug.LogWarning("SfxDrops.cs: No pickup sound has been assigned to this drop.");
            }
        }
    }
}
