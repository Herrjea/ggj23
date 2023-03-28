using UnityEngine.Events;

//Clase estática que contiene los distintos eventos que pueden lanzarse en el juego
public static class GameEvents
{
    #region Howto

    /**** Ejemplo de evento sin parámetros
     *
     * 
     * public static UnityEvent [[EventName]] = new UnityEvent();
     * 
     */

    /**** Ejemplo de evento con parámetros específicos
     * 
     * 
     * 1 --> Creo la clase a la que pertenecerá el evento
     * 
     * public class [[MyEventType]] : UnityEvent<[[param_type]]>
     * 
     * 2 --> Creo el evento
     * 
     * public static [[MyEventType]] [[EventName]] = new [[MyEventType]]();
     * 
     */

    /**** Ejemplo de evento con parámetros especificados por una clase
     * 
     * 
     * 1 --> Creo la clase que contendrá los parámetros
     * 
     * public class [[MyEventType]]Data 
     * {
     * 
     *     int _param1;
     *     float _param2;
     *     
     *     public [[MyEventType]]Data(int param1, float param2)
     *     {
     *         _param1 = param1;
     *         _param2 = param2;
     *     }
     * }
     * 
     * 2 --> Creo la clase a la que pertenecerá el evento
     * 
     * public class [[MyEventType]] : UnityEvent<[[MyEventType]]Data>{};
     * 
     * 
     * 3 --> Creo el evento
     * 
     * public static [[MyEventType]] [[EventName]] = new [[MyEventType]]();
     * 
     */

    #endregion


    #region Needed types

    public class FloatEvent : UnityEvent<float> { };
    public class Float4Event : UnityEvent<float, float, float, float> { };
    public class InputEvent : UnityEvent<Input> { };
    public class IntEvent : UnityEvent<int> { };
    public class Int4Event : UnityEvent<int, int, int, int> { };
    public class IntArrayEvent : UnityEvent<int[]> { };
    public class BoolArrayEvent : UnityEvent<bool[]> { };

    #endregion


    //
    // Input reading
    //

    //    public static UnityEvent ChangeMovementType = new UnityEvent();


    //
    // Gameplay
    //

    public static UnityEvent InteractionRequested = new UnityEvent();

    public static UnityEvent UIInteractionRequested = new UnityEvent();
    public static UnityEvent UIBackRequested = new UnityEvent();

    public static InputEvent InputSet = new InputEvent();

    public static UnityEvent ShowControls = new UnityEvent();
    public static UnityEvent HideControls = new UnityEvent();

    public static IntArrayEvent P1NewCode = new IntArrayEvent();
    public static IntArrayEvent P2NewCode = new IntArrayEvent();

    public static IntArrayEvent P1NewTypeHistory = new IntArrayEvent();
    public static IntArrayEvent P2NewTypeHistory = new IntArrayEvent();

    public static BoolArrayEvent P1OwnTypeHistoryDisplay = new BoolArrayEvent();
    public static BoolArrayEvent P1EnemyTypeHistoryDisplay = new BoolArrayEvent();
    public static BoolArrayEvent P2OwnTypeHistoryDisplay = new BoolArrayEvent();
    public static BoolArrayEvent P2EnemyTypeHistoryDisplay = new BoolArrayEvent();

    public static IntEvent P1KeyPress = new IntEvent();
    public static IntEvent P2KeyPress = new IntEvent();

    public static IntEvent P1RightPress = new IntEvent();
    public static IntEvent P2RightPress = new IntEvent();

    public static IntEvent P1WrongPress = new IntEvent();
    public static IntEvent P2WrongPress = new IntEvent();

    public static UnityEvent P1OwnWordCompleted = new UnityEvent();
    public static UnityEvent P2OwnWordCompleted = new UnityEvent();

    public static UnityEvent P1EnemyWordCompleted = new UnityEvent();
    public static UnityEvent P2EnemyWordCompleted = new UnityEvent();

    public static IntEvent P1LvlChange = new IntEvent();
    public static IntEvent P2LvlChange = new IntEvent();

    public static UnityEvent P1Wins = new UnityEvent();
    public static UnityEvent P2Wins = new UnityEvent();
}
