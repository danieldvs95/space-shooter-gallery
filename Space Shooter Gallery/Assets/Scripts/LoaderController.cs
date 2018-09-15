using UnityEngine;

public class LoaderController : MonoBehaviour
{

    public Animator animator;

	public void FadeOut()
    {
        if (animator != null)
        {
            animator.SetBool("fadeOut", true);
        }
    }
}
