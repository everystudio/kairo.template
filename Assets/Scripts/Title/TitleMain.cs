using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using anogame;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class TitleMain : StateMachineBase<TitleMain>
{
    [SerializeField] private Button newgameButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button resumeButton;

    private void Start()
    {
        ChangeState(new TitleMain.Newtral(this));
    }

    private class Newtral : StateBase<TitleMain>
    {
        public Newtral(TitleMain machine) : base(machine)
        {
        }

        public override void OnEnterState()
        {
            machine.newgameButton.onClick.AddListener(() =>
            {
                machine.ChangeState(new TitleMain.NewGame(machine));
            });
            machine.continueButton.onClick.AddListener(() =>
            {
                machine.ChangeState(new TitleMain.Continue(machine));
            });
            machine.resumeButton.onClick.AddListener(() =>
            {
                machine.ChangeState(new TitleMain.Resume(machine));
            });
        }
        override public void OnExitState()
        {
            machine.newgameButton.onClick.RemoveAllListeners();
            machine.continueButton.onClick.RemoveAllListeners();
            machine.resumeButton.onClick.RemoveAllListeners();
        }
    }

    private class NewGame : StateBase<TitleMain>
    {
        public NewGame(TitleMain machine) : base(machine)
        {
        }
    }

    private class Continue : StateBase<TitleMain>
    {
        public Continue(TitleMain machine) : base(machine)
        {
        }
    }

    private class Resume : StateBase<TitleMain>
    {
        public Resume(TitleMain machine) : base(machine)
        {
        }

        public override void OnEnterState()
        {

            machine.StartCoroutine(LoadGame());

        }

        private IEnumerator LoadGame()
        {
            //AsyncOperation asyncOperationGame = SceneManager.LoadSceneAsync("Field01-01Home", LoadSceneMode.Additive);
            AsyncOperation asyncOperationGame = SceneManager.LoadSceneAsync("Field01-01Home", LoadSceneMode.Single);
            asyncOperationGame.allowSceneActivation = true;
            while (asyncOperationGame.progress != 0.9f)
            {
                yield return null;
            }

            /*

            AsyncOperation asyncOperationCore = SceneManager.LoadSceneAsync("Core", LoadSceneMode.Additive);
            asyncOperationCore.allowSceneActivation = false;
            while (asyncOperationCore.progress != 0.9f)
            {
                yield return null;
            }

            asyncOperationGame.allowSceneActivation = true;
            yield return new WaitUntil(() => asyncOperationGame.isDone);

            asyncOperationCore.allowSceneActivation = true;
            yield return new WaitUntil(() => asyncOperationCore.isDone);



            AsyncOperation unloadPreviousScene = SceneManager.UnloadSceneAsync("Title");
            yield return new WaitUntil(() => unloadPreviousScene.isDone);

            SceneManager.SetActiveScene(SceneManager.GetSceneByName("Field01-01Home"));
            */
        }
    }
}
