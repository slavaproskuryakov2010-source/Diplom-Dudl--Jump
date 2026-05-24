
   using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Вызывается по нажатию кнопки Start
    public void StartGame()
    {
        // Загружаем сцену с игрой (имя должно совпадать с названием сцены в Build Settings)
        SceneManager.LoadScene("Game");
    }

    // Вызывается по нажатию кнопки Exit
    public void ExitGame()
    {
        // Закрывает приложение в собранной игре (на ПК, телефоне и т.д.)
        Application.Quit();

        // Чтобы кнопка работала и в редакторе Unity:
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
} 

