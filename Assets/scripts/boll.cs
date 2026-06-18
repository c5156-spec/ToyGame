using UnityEngine;
using UnityEngine.InputSystem; // Input Systemを使用するために必要

public class BowlingBall : MonoBehaviour
{
    private Rigidbody rb;
    public float forwardForce = 20f; // 前進する力
    public float torqueForce = 10f;  // 回転させる力
    private bool isFired = false;

    private BowlingInputActions bowlingInputActions; // 自動生成されるInput Actionsクラスのインスタンス

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("BowlingBallにRigidbodyコンポーネントがありません。Rigidbodyを追加してください。");
            // Rigidbodyがない場合でもInput Systemのセットアップは続行しますが、
            // FireBallメソッド内でrbのnullチェックを行い、NREを防ぎます。
        }
        
        // Input Actionsをインスタンス化
        bowlingInputActions = new BowlingInputActions();

        // Fireアクションが実行されたときにFireBallメソッドを呼び出すように設定
        // performedイベントはボタンが押された瞬間に一度だけ発火します
        bowlingInputActions.botton.Newaction.performed += _ => FireBall();
    }

    void OnEnable()
    {
        // MonoBehaviourが有効になったときにInput Actionsも有効にする
        bowlingInputActions.Enable();
    }

    void OnDisable()
    {
        // MonoBehaviourが無効になったときにInput Actionsも無効にする
        bowlingInputActions.Disable();
    }

    void FireBall()
    {
        // 既に発射済みであれば何もしない
        if (isFired)
        {
            return;
        }
        
        // Rigidbodyがない場合は処理しない
        if (rb == null)
        {
            Debug.LogError("BowlingBallのRigidbodyがnullのため、ボールを発射できません。");
            return;
        }

        // +X方向に力を加える
        rb.AddForce(Vector3.right * forwardForce, ForceMode.Impulse);

        // Z軸を中心に回転させる力を加える（縦回転）
        // ※進む方向と回転の向きは、シーンの配置に合わせて Vector3.forward や Vector3.back に調整してください
        rb.AddTorque(Vector3.forward * torqueForce, ForceMode.Impulse);

        // ボールが発射されたことをマーク
        isFired = true;

        // ボールが一度発射されたら、それ以上発射できないようにInput Actionを無効化する
        // これにより、FireBallが複数回呼び出されるのを防ぎます
        bowlingInputActions.botton.Newaction.Disable();
    }
}
