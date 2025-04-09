using UnityEngine;

public class Firebolt : SpellBaseBehaviour
{
    private void Update()
    {
        if (isActive)
        {
            transform.position = Vector2.MoveTowards(transform.position, destination.transform.position, spellData.GetSpeed() * Time.deltaTime);

            if (Vector2.Distance(transform.position, destination.transform.position) < 0.1f)
            {
                Destroy(gameObject);
                destination.TakeDamage(spellData.GetDamage());
            }
        }
    }
    public override void CastSpell()
    {
        FaceEnemy();
        this.transform.position = source.transform.position;
        isActive = true;
        Debug.Log("Casting Firebolt!");
        
    }

    void FaceEnemy()
    {
        Vector2 direction = destination.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
