using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JMove : MonoBehaviour,IPointerDownHandler, IPointerUpHandler, IDragHandler{

    RectTransform rect; //joystick background object
    Vector2 touch = Vector2.zero;// normalized mounse touch position from canvuce angle 
    public RectTransform handle;// joystick handle object
    float widthHalf;// joystick handle can't out from rect due to this var

    public JoystickValue value; //it is moved object by handle

    private void Start()
    {
        rect = GetComponent<RectTransform>(); 
        widthHalf = rect.sizeDelta.x * 0.5f;
    }
    public void OnDrag(PointerEventData eventData)//touch and moving mouce   1280 * 720   ���� �ػ� x * y    x / 1280  y720 
    {
        float x_  = (float)Screen.width / 1920f; // ���� �ػ󵵿� ���� ���� �ػ� ���� ����
        float y_= (float)Screen.height / 1080f;  // ���� �ػ󵵿� ���� ���� �ػ� ���� ����
        Vector2 primeVector =  (eventData.position - new Vector2(rect.position.x,rect.position.y)); // ���� �ػ󵵿� �´� ���̽�ƽ ������ ũ��, �� ���� ���� ũ��
        float RatioC = (new Vector2(x_, y_)).magnitude / (new Vector2(1, 1)).magnitude; // ���̽�ƽ ������ ũ�� ���� (���� �ػ� ���� ũ�⸦ 1��), Clean���ε� ��� ���������� �ڵ� ��������, ��� ���� ȭ�� �ػ� ���μ����� ���������� ���ͷ� ���� ��
        
        Vector2 Clean = new Vector2((eventData.position.x  * x_)- (rect.position.x * x_), (eventData.position.y * y_) - (rect.position.y* y_)); // / widthHalf ����. ������ normalized�ؼ� ���, touch���� / widthHalf
   
        touch = Clean.normalized * primeVector.magnitude / (widthHalf * RatioC); // Clean�� ���⸸ �������� ���� ���� ũ��� ����. 
                                                                              // widthHarf�� ���� �ػ� ������ �°� ����.
        

        if(touch.magnitude > 1) 
            touch = touch.normalized;// joystick handle can't out from rect due to this code
        value.joyTouch = touch.normalized; // moved object by handle put in touch point
        handle.anchoredPosition = touch * widthHalf;// it's show moved handle 
    }
    public void OnPointerDown(PointerEventData eventData) //only touch starting moment
    {
       
        OnDrag(eventData);
    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    public void OnPointerUp(PointerEventData eventData)//only touch outing moment
    {
     
        
        // handle.anchoredPosition = Vector2.zero;// handle is moved 0,0 point
        // value.joyTouch = Vector2.zero;// stop moving moved object by handle object
      

    }
}
