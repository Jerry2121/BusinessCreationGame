using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Game Changes")]
    public int TimeBetweenDays;
    private float TimerBetweenDays;
    public float WarningDisplayTime;
    public bool Warning;
    public bool Gameover;
    public bool SpeedUp;
    public int SpeedUpMultiplier;
    [Header("Player stats")]
    public int Money;
    public int Day;
    public int BillDay;
    public int Employees;
    public int Businesslocations;
    [Header("Game Objects (Money and Stats)")]
    public TextMeshProUGUI MoneyText;
    public TextMeshProUGUI EmployeeText;
    public TextMeshProUGUI BusinesslocationText;
    public TextMeshProUGUI DayText;
    public GameObject SpeedUpUI;
    public GameObject GameOverUI;
    public GameObject FiringUI;
    public GameObject NewBusinessUI;
    public GameObject NotEnoughMoneyUI;
    [Header("Tips Control")]
    public bool ShowTips;
    public bool ShowTip1;
    public bool ShowTip2;
    public bool ShowTip3;
    public bool ShowTip4;
    public float TipShowTimeBeforeSwitch;
    private float TipShowTime;
    public GameObject Tip1;
    public GameObject Tip2;
    public GameObject Tip3;
    public GameObject Tip4;
    // Start is called before the first frame update
    void Start()
    {
        TimerBetweenDays = TimeBetweenDays;
        TipShowTime = TipShowTimeBeforeSwitch;
    }

    // Update is called once per frame
    void Update()
    {
        if (ShowTips)
        {
            TipShowTime -= Time.deltaTime;
        }
        if (ShowTips && !ShowTip1 && !ShowTip2 && !ShowTip3 && !ShowTip4)
        {
            Tip1.SetActive(true);
            Tip2.SetActive(false);
            Tip3.SetActive(false);
            Tip4.SetActive(false);
            TipShowTime = TipShowTimeBeforeSwitch;
            ShowTip1 = true;
        }
        if (TipShowTime <= 0 && ShowTips && ShowTip1 && !ShowTip2 && !ShowTip3 && !ShowTip4)
        {
            Tip1.SetActive(false);
            Tip2.SetActive(true);
            Tip3.SetActive(false);
            Tip4.SetActive(false);
            TipShowTime = TipShowTimeBeforeSwitch;
            ShowTip2 = true;
        }
        if (TipShowTime <= 0 && ShowTips && ShowTip1 && ShowTip2 && !ShowTip3 && !ShowTip4)
        {
            Tip1.SetActive(false);
            Tip2.SetActive(false);
            Tip3.SetActive(true);
            Tip4.SetActive(false);
            TipShowTime = TipShowTimeBeforeSwitch;
            ShowTip3 = true;
        }
        if (TipShowTime <= 0 && ShowTips && ShowTip1 && ShowTip2 && ShowTip3 && !ShowTip4)
        {
            Tip1.SetActive(false);
            Tip2.SetActive(false);
            Tip3.SetActive(false);
            Tip4.SetActive(true);
            TipShowTime = TipShowTimeBeforeSwitch;
            ShowTip4 = true;
        }
        if (TipShowTime <= 0 && ShowTips && ShowTip1 && ShowTip2 && ShowTip3 && ShowTip4)
        {
            ShowTip1 = false;
            ShowTip2 = false;
            ShowTip3 = false;
            ShowTip4 = false;
        }
        if (Warning)
        {
            WarningDisplayTime += Time.deltaTime;
        }
        if (WarningDisplayTime >= 3 && Warning)
        {
            NotEnoughMoneyUI.SetActive(false);
            FiringUI.SetActive(false);
            NewBusinessUI.SetActive(false);
            Warning = false;
        }
        if (Money <= -200)
        {
            Gameover = true;
        }
        if (Gameover)
        {
            GameOverUI.SetActive(true);
        }
        if (SpeedUp && !Gameover)
        {
            TimerBetweenDays -= Time.deltaTime * SpeedUpMultiplier;
        }
        else if (!Gameover)
        {
            TimerBetweenDays -= Time.deltaTime;
        }
        MoneyText.text = "Your Money: $" + Money;
        DayText.text = "Day: " + Day;
        EmployeeText.text = "Hired Employees: " + Employees;
        BusinesslocationText.text = "Business Locations: " + Businesslocations;
        if (TimerBetweenDays <= 0 && !Gameover)
        {
            PayDay();
            TimerBetweenDays = TimeBetweenDays;
        }
        if (BillDay == 30 && !Gameover)
        {
            Money -= Employees * 200;
            Money -= Businesslocations * 600;
            BillDay = 0;
        }
    }
    public void PayDay()
    {
        Money += Employees * 50;
        Day++;
        BillDay++;
    }

    public void FireEmployee()
    {
        if (Employees >= 2)
        {
            Employees--;
        }
        else
        {
            Warning = true;
            WarningDisplayTime = 0;
            FiringUI.SetActive(true);
        }
    }
    public void HireEmployee()
    {
        if (Money - 2000 >= -200)
        {
            Money = Money - 2000;
            Employees++;
        }
        else
        {
            Warning = true;
            WarningDisplayTime = 0;
            NotEnoughMoneyUI.SetActive(true);
        }
    }
    public void SellLocation()
    {
        if (Businesslocations >= 2)
        {
            Money = Money - 3000;
            Businesslocations--;
        }
        else
        {
            Warning = true;
            WarningDisplayTime = 0;
            NewBusinessUI.SetActive(true);
        }
    }
    public void NewBusinessLocation()
    {
        if (Money - 10000 >= -200)
        {
            Money = Money - 10000;
            Businesslocations++;
        }
        else
        {
            Warning = true;
            WarningDisplayTime = 0;
            NotEnoughMoneyUI.SetActive(true);
        }
    }
    public void Speedup()
    {
        SpeedUpUI.SetActive(!SpeedUpUI.activeSelf);
        SpeedUp = !SpeedUp;
    }
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
