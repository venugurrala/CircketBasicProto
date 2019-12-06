using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameControls : MonoBehaviour
{

    public static GameControls _inst;

    public Transform ballLeft, ballRight;
    public Bowling _bowling;

    public Text txt_offVal;
    public Text txt_BallSpeed;
    public Text txt_BatSpeed;
   public Slider _SpeedSlider;
    public Slider _sliderOffVal;
    public Slider _sliderBatSpeed;
    bool isRight = false;

    public Button _btnThrow;
    private void Awake()
    {
        
        SpeedSliderValChange();
        StrightBowling();
       // OffVal();
    }
    private void Start()
    {
        BatHitSpeedControler();
    }
    public void ThrowBall()
    {
        BatMovement._inst.DisabelBatCollider();
        _btnThrow.gameObject.SetActive(false);
        _bowling.ThrowBall();
    }

    public void Reset()
    {
        _btnThrow.gameObject.SetActive(true);
        _bowling.ReSetToThrowTheBall();
        SideSelect();
        StrightBowling();
    }

    public void SwitchSede()
    {
        isRight = !isRight;
        SideSelect();
    }
    void SideSelect()
    {
        if (isRight)
        {
            // Debug.LogError("hi");
            _bowling.gameObject.transform.position = ballRight.position;
        }
        else
        {
            //Debug.LogError("hellow");
            _bowling.gameObject.transform.position = ballLeft.position;
        }
    }

    public void SpeedSliderValChange()
    {
        txt_BallSpeed.text = 18 + (_SpeedSlider.value * 100) +80+ ""; //maintain the value 100 -200

        _bowling.ballSpeed =18+ (_SpeedSlider.value * 10);// 20 to 30 only

    }

    public void OffVal()
    {
        // _sliderOffVal.value = Mathf.Clamp(_sliderOffVal.value, 0f, 1f);
        if (_sliderOffVal.value < 0.1)
        {
            _bowling.offval = -10;
            txt_offVal.text = _bowling.offval.ToString();
            return;
        }

        if (_sliderOffVal.value > 0.5)
        {

            _bowling.offval = (_sliderOffVal.value / 0.5f) * 5;
        }
        else if (_sliderOffVal.value < 0.5)
        {
            _bowling.offval = - (((0.5f / _sliderOffVal.value) * 1) + 5);
        }

        txt_offVal.text = _bowling.offval.ToString();
        /*  _sliderOffVal.value = Mathf.Clamp(_sliderOffVal.value, 0.1f, 0.9f);

          _sliderOffVal.value = 0.5f;
          _bowling.offval = 5;*/
    }

    public void StrightBowling()
    {

        _sliderOffVal.value = 0.5f;
        _bowling.offval = 5;
        txt_offVal.text = _bowling.offval.ToString();


    }

    public void BatHitSpeedControler()
    {
        _sliderBatSpeed.value = Mathf.Clamp(_sliderBatSpeed.value,0.1f,1);
        BatMovement._inst._batSpeed = _sliderBatSpeed.value * 100;
        txt_BatSpeed.text =" Bat hit speed : "+(int) BatMovement._inst._batSpeed + "";
    }

}
