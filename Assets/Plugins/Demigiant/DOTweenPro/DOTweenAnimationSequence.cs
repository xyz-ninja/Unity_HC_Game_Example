using DG.Tweening;
using UnityEngine;

public class DOTweenAnimationSequence : MonoBehaviour
{
    public DOTweenAnimation[] Animations;

    private void OnValidate()
    {
        
    }

    public void PlayAll()
    {
        for (int i = 0; i < Animations.Length - 1; i++)
        {
            if (Application.isPlaying == false)
            {
                Animations[i].CreateTween();
         //       DOTweenEditorPreview.PrepareTweenForPreview(Animations[i].tween);
            }
            
            Animations[i].tween.onComplete += Animations[i + 1].DOPlay;
        }

        if(Application.isPlaying)
        {
            Animations[0].tween.Play();
        }
        else
        {
        //    DOTweenEditorPreview.Start();
        }
    }
}