using UnityEngine;

[CreateAssetMenu]
public class ParamsSO : ScriptableObject
{
    [Header("制限時間(s)")]
    [SerializeField]
    private int _timeLimit;

    public int TimeLimit
    {
        get { return _timeLimit; }
        set { _timeLimit = value; }
    }

    [Header("初期のボールの数")]
    [SerializeField]
    private int _initBallCount;

    public int InitBallCount
    {
        get { return _initBallCount; }
        set { _initBallCount = value; }
    }

    [Header("消せる最小個数")]
    [SerializeField]
    private int _ballMinNumber;

    public int BallMinNumber
    {
        get { return _ballMinNumber; }
        set { _ballMinNumber = value; }
    }

    [Header("ボールを消した時の得点")]
    [SerializeField]
    private int _scorePoint;

    public int ScorePoint
    {
        get { return _scorePoint; }
        set { _scorePoint = value; }
    }

    [Header("ボールの判定距離")]
    [SerializeField]
    private float _ballDistance;

    public float BallDistance
    {
        get { return _ballDistance; }
        set { _ballDistance = value; }
    }

    [Header("ボムの爆破範囲")]
    [SerializeField]
    [Range(1, 3)]
    private float _bombRange;

    public float BombRange
    {
        get { return _bombRange; }
        set { _bombRange = value; }
    }

    [Header("ボムが出る確率(%)")]
    [SerializeField]
    private int _bombRate;

    public int BombRate
    {
        get { return _bombRate; }
        set { _bombRate = value; }
    }

    //MyScriptableObjectが保存してある場所のパス
    public const string PATH = "ParamsSO";

    //MyScriptableObjectの実体
    private static ParamsSO _entity;
    public static ParamsSO Entity
    {
        get
        {
            //初アクセス時にロードする
            if (_entity == null)
            {
                _entity = Resources.Load<ParamsSO>(PATH);

                //ロード出来なかった場合はエラーログを表示
                if (_entity == null)
                {
                    Debug.LogError(PATH + " not found");
                }
            }

            return _entity;
        }
    }
}