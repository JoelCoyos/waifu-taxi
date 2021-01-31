﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionDialogueManager : MonoBehaviour
{

    [SerializeField] private QuestionInput questionInput;

    public void Start()
    {

    }

    public IEnumerator showQuestionRoutine(QuestionDialogue dialogue)
    {
            int k=dialogue.Options.Length;
            string fullDialogue=null;
            questionInput.askQuestion(dialogue.Correct);
            for(int i=0;i<=k;i++)
            {
                fullDialogue=" ";
                //Dialogo inicial
                if(i==1)
                {
                }
                if(i==0)
                {
                    fullDialogue+=dialogue.Text;
                }
                //Dialogo final
                else if(i==k)
                {
                    //fullDialogue+=dialogue.AfterDialogue;
                    yield return new WaitForSeconds(2);
                }
                //Las opciones
                else
                {
                    fullDialogue=dialogue.Options[i-1];
                }
                yield return new WaitUntil(()=>DialogueManager.I.canShowUrgentDialogue);
                StartCoroutine(DialogueManager.I.showDialogue(fullDialogue,true));
            }
            yield return new WaitUntil(()=>DialogueManager.I.canShowUrgentDialogue);
            if(questionInput.answeredCorrectly())
            {
                //ScoreManager.I.addStar(1);
                fullDialogue = dialogue.CorrectDialogue;
            }
            else
            {
                //ScoreManager.I.addStar(-1);
                fullDialogue=dialogue.FailDialogue;
            }
            StartCoroutine(DialogueManager.I.showDialogue(fullDialogue,true));
            yield return new WaitUntil(()=>DialogueManager.I.canShowUrgentDialogue);
            DialogueManager.I.canShowQuestion=false;
            DialogueManager.I.needsUrgentDialogue=false;
    }
}