using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public static int level;
    public static long money;
    public static int jewel;
    public static int ticket;
    public static int chef_Number;

    

    public Vector2 limitP1;
    public Vector2 limitP2;

    public Sprite[] Burger_Sprite;

    public Text textMoney;
    public Text textJewel;
    public Text textTicket;
    public Text text;
    public Sprite Profile;
    public List<GameObject> preChefs;
    public GameObject preChef;
    public GameObject Kitchen_Control_Panel;
    public GameObject Chef_Manage_Panel;
    public GameObject Chef_Hire_Panel;
    public GameObject Settings_Panel;
    public GameObject Not_Money_Panel;
    public GameObject Level_Up_Panel;
    public GameObject Chef_Select_Panel;
    public GameObject Chef_Fire_Panel;
    

    public List<Chef> chefs;
    public static string FirstName
    {
        get
        {
            string[] names = new string[10];

            names[0] = "김";
            names[1] = "이";
            names[2] = "박";
            names[3] = "최";
            names[4] = "양";
            names[5] = "정";
            names[6] = "유";
            names[7] = "강";
            names[8] = "조";
            names[9] = "임";

            int r = Random.Range(0, names.Length);
            string s = names[r];

            return s;
        }
    }

    public static string LastName
    {
        get
        {
            string[] names = new string[10];

            names[0] = "철수";
            names[1] = "영희";
            names[2] = "명수";
            names[3] = "유리";
            names[4] = "맹구";
            names[5] = "짱구";
            names[6] = "수민";
            names[7] = "숙희";
            names[8] = "민";
            names[9] = "경희";

            int r = Random.Range(0, names.Length);
            string s = names[r];

            return s;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        chefs = new List<Chef>();
        textTicket.text = "10";
        jewel = 1000;
        money = 10000;
        level = 1;
        chef_Number = chefs.Count;

        //text = Not_Money_Panel.transform.Find("Text").GetComponent<Text>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(EventSystem.current.IsPointerOverGameObject() == false)
        { 
            EarnMoney();
        }
        ShowTextMoney();
        ShowTextJewel();
    }
    private void OnDrawGizmos()
    {
        Vector2 limitP3 = new Vector2(limitP2.x, limitP1.y);
        Vector2 limitP4 = new Vector2(limitP1.x, limitP2.y);

        Gizmos.color = Color.red;

        Gizmos.DrawLine(limitP1, limitP3);
        Gizmos.DrawLine(limitP3, limitP2);
        Gizmos.DrawLine(limitP1, limitP4);
        Gizmos.DrawLine(limitP4, limitP2);

    }

    public void EarnMoney()
    {
        if (Input.GetMouseButtonDown(0))
        {
            money += 5;
            Debug.Log(money);
            Debug.Log(chefs.Count);
        }
    }

    public void ShowTextMoney()
    {
        textMoney.text = money.ToString("###,###");
    }

    public void ShowTextJewel()
    {
        textJewel.text = jewel.ToString("###,###");
    }

    Chef RandomCreateChef()
    {
        Chef info = new Chef();

        info.name = FirstName + LastName;

        info.level = 0;

        info.knife = Random.Range(30, 60);
        info.pan = Random.Range(30, 60);
        info.wash = Random.Range(30, 60);

        info.salary = Random.Range(500, 1000);

        int r = Random.Range(0, 2);
        info.gender = (Gender)r;

        return info;
    }

    public void CreateChef(Chef chef)
    {
        GameObject obj = Instantiate(preChef, Vector3.zero, Quaternion.identity);
        obj.GetComponent<ChefControl>().info = chef;
        preChefs.Add(obj);
    }

    public void InitializeChef(Chef chef)
    {
        GameObject obj = Instantiate(preChef, Vector3.zero, Quaternion.identity);
        obj.GetComponent<ChefControl>().info = chef;
    }

    public void ChefHireOpen()
    {
        Chef_Hire_Panel.SetActive(true);

        Chef chef = RandomCreateChef();

        var textName = Chef_Hire_Panel.transform.Find("Name/Chef_Name").GetComponent<Text>();
        var textKnife = Chef_Hire_Panel.transform.Find("Knife/Knife_Ability").GetComponent<Text>();
        var textPan = Chef_Hire_Panel.transform.Find("Pan/Pan_Ability").GetComponent<Text>();
        var textWash = Chef_Hire_Panel.transform.Find("Wash/Wash_Ability").GetComponent<Text>();
        var textSalary = Chef_Hire_Panel.transform.Find("Salary/Chef_Salary").GetComponent<Text>();
        var HireButton = Chef_Hire_Panel.transform.Find("Chef_Hire_Ok").GetComponent<Button>();
       
        textName.text = chef.name;
        textKnife.text = chef.knife.ToString();
        textPan.text = chef.pan.ToString();
        textWash.text = chef.wash.ToString();
        textSalary.text = chef.salary.ToString();

        HireButton.onClick.RemoveAllListeners();
        HireButton.onClick.AddListener(delegate
        {
            Debug.Log(money);
            Hire((int)chef.salary, chef);
        });
    }

    public void Hire(int price, Chef chef)
    {
        if(level < 3)
        {
            if (chefs.Count < 2)
            {
                if (money >= price)
                {
                    CreateChef(chef);
                    money -= price;
                    chefs.Add(chef);
                    Chef_Hire_Panel.SetActive(false);
                }
                else
                {
                    text.text = ("금액이 부족합니다.");
                    Not_Money_Panel.SetActive(true);

                }
            }
            else
            {
                text.text = ("더 이상 고용할 수 없습니다.");
                Not_Money_Panel.SetActive(true);
            }
        }
        else if(level >= 3 || level < 8)
        {
            if (chefs.Count < 3)
            {
                if (money >= price)
                {
                    CreateChef(chef);
                    money -= price;
                    chefs.Add(chef);
                    Chef_Hire_Panel.SetActive(false);
                }
                else
                {
                    text.text = ("금액이 부족합니다.");
                    Not_Money_Panel.SetActive(true);

                }
            }
            else
            {
                text.text = ("더 이상 고용할 수 없습니다.");
                Not_Money_Panel.SetActive(true);
            }
        }
    }

    public void ChefSelectOpen()
    {
        Chef_Select_Panel.SetActive(true);

        for(int i=0;i<chefs.Count;i++)
        {
            Debug.Log("Chef_Select_" + i + 1 + "/Chef_Name_" + i + 1);
            var TextName = Chef_Select_Panel.transform.Find("Chef_Select_" + i+1 + "/Chef_Name_" + i+1).GetComponent<Text>();
            var ImgChef = Chef_Select_Panel.transform.Find("Chef_Select_" + i+1 + "/Chef_Profile_" + i+1).GetComponent<Image>();
            
            TextName.text = chefs[i].name;
            ImgChef.sprite = Profile;
        }
    }

    public void ChefFireOpen1()
    {
        Chef_Fire_Panel.SetActive(true);

        var TextName = Chef_Fire_Panel.transform.Find("Name/Chef_Name").GetComponent<Text>();
        var TextSalary = Chef_Fire_Panel.transform.Find("Salary/Chef_Salary").GetComponent<Text>();
        var FireButton = Chef_Fire_Panel.transform.Find("Chef_Fire_Ok").GetComponent<Button>();

        TextName.text = chefs[0].name;
        TextSalary.text = chefs[0].salary.ToString();

        FireButton.onClick.RemoveAllListeners();
        FireButton.onClick.AddListener(delegate
        {
            Fire(chefs[0], 0);
        });
    }

    public void Fire(Chef chef, int index)
    {       
        chefs.Remove(chef);
        Destroy(preChefs[index]);
        Chef_Fire_Panel.SetActive(false);
    }
    
    public void LevelUp()
    {
        if(level == 1)
        {
            if(money >= 500)
            {
                level++;
                money -= 500;
                var textLevel = Level_Up_Panel.transform.Find("Text/Text_Level").GetComponent<Text>();
                var textChange = Level_Up_Panel.transform.Find("Text/Text_Change").GetComponent<Text>();

                textLevel.text = level.ToString();
                textChange.text = "대회 참가 가능!";
                Level_Up_Panel.SetActive(true);

            }
            else
            {
                text.text = ("금액이 부족합니다.");
                Not_Money_Panel.SetActive(true);
            }
        }
        else if(level == 2)
        {
            if (money >= 1500)
            {
                level++;
                money -= 1500;
                var textLevel = Level_Up_Panel.transform.Find("Text/Text_Level").GetComponent<Text>();
                var textChange = Level_Up_Panel.transform.Find("Text/Text_Change").GetComponent<Text>();

                textLevel.text = level.ToString();
                textChange.text = "최대 직원 2 -> 3";
                Level_Up_Panel.SetActive(true);

            }
            else
            {
                text.text = ("금액이 부족합니다.");
                Not_Money_Panel.SetActive(true);
            }
        }
        else if (level == 3)
        {
            if (money >= 2500)
            {
                level++;
                money -= 2500;
                var textLevel = Level_Up_Panel.transform.Find("Text/Text_Level").GetComponent<Text>();
                var textChange = Level_Up_Panel.transform.Find("Text/Text_Change").GetComponent<Text>();

                textLevel.text = level.ToString();
                textChange.text = "";
                Level_Up_Panel.SetActive(true);

            }
            else
            {
                text.text = ("금액이 부족합니다.");
                Not_Money_Panel.SetActive(true);
            }
        }
        
    }

    public void SceneChange()
    {
        ticket--;
        SceneManager.LoadScene("contest");
    }
    

    public void Contest()
    {

    }
}
