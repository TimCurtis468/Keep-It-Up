using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    public static int score;


    public GameObject hsTable;
    public GameObject Button;

    private float widthFactor;
    private float heightFactor;


    public void Awake()
    {
        GameObject obj;
        Transform trans;
        Transform childTrans;

        widthFactor = Screen.width / 1920.0f;
        heightFactor = Screen.height / 1080.0f;

        HighscoreTable.textFactor = (HighscoreTable.textFactor * Screen.height) / 1080;

        // Highscore table
        Resize(hsTable);

        // Title background
        trans = hsTable.transform;
        childTrans = trans.Find("background");
        obj = childTrans.gameObject;
        Resize(obj);

        // Title background
        trans = hsTable.transform;
        childTrans = trans.Find("TitleBackground");
        obj = childTrans.gameObject;
        Resize(obj);

        // Title text
        trans = hsTable.transform;
        childTrans = trans.Find("TitleText");
        obj = childTrans.gameObject;
        Resize(obj);
        ResizeText(obj);

        // Title text
        trans = hsTable.transform;
        childTrans = trans.Find("GameScoreText");
        obj = childTrans.gameObject;
        Resize(obj);
        ResizeText(obj);

        // Highscore Entry Container
        trans = hsTable.transform;
        childTrans = trans.Find("highscoreEntryContainer");
        obj = childTrans.gameObject;
        Resize(obj);

        // Button
        Resize(Button);

        // Button text
        // Title text
        trans = Button.transform;
        childTrans = trans.Find("Text");
        obj = childTrans.gameObject;
        Resize(obj);
        ResizeText(obj);
    }

    public void ChangeMenuScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    private void Resize(GameObject gameObject)
    {
        float objectWidth;
        float objectHeight;
        RectTransform rt;

        rt = gameObject.GetComponent<RectTransform>();
        objectWidth = rt.rect.width;
        objectHeight = rt.rect.height;

        objectWidth = objectWidth * widthFactor;
        objectHeight = objectHeight * heightFactor;

        rt.sizeDelta = new Vector2(objectWidth, objectHeight);
        rt.anchoredPosition = new Vector2(rt.anchoredPosition.x * widthFactor, rt.anchoredPosition.y * heightFactor);
    }

    private void ResizeText(GameObject gameObject)
    {
        Text t = gameObject.GetComponent<Text>();
        t.fontSize = (t.fontSize * Screen.width) / 1920;
    }

}
