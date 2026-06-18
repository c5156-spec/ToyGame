using UnityEngine;

public class pin : MonoBehaviour
{
    private Rigidbody rb; // Rigidbodyコンポーネントへの参照
    public float impactForce = 10f; // 衝突で吹き飛ばす力
    public float sidewaysForce = 5f; // 横に吹き飛ばす力

    void Start()
    {
        // 自分のRigidbodyを取得
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("ピンにRigidbodyコンポーネントがありません。Rigidbodyを追加してください。");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // 衝突したオブジェクトのタグが "Player" であるかを確認
        if (collision.gameObject.CompareTag("Player"))
        {
            if (rb == null) return; // Rigidbodyがない場合は処理しない

            // 最初の接触点情報を取得
            ContactPoint contact = collision.contacts[0];

            // 衝突した方向（法線）の逆方向への力を計算
            Vector3 forceDirection = -contact.normal;

            // 横方向の力を計算
            // 衝突法線とVector3.upの外積を取り、水平な横方向ベクトルを得る
            // これにより、ボールがピンのどちら側に当たったかに応じて、横方向の力が調整される
            Vector3 lateralDirection = Vector3.Cross(contact.normal, Vector3.up).normalized;

            // 力の合計を計算
            Vector3 totalForce = forceDirection * impactForce + lateralDirection * sidewaysForce;

            // 計算した力をピンに加える
            rb.AddForce(totalForce, ForceMode.Impulse);
        }
    }
}
