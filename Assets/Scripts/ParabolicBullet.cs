public class ParabolicBullet : Bullet
{
    private void FixedUpdate()
    {
        if(Rb.velocity.magnitude >1)
        transform.forward = Rb.velocity.normalized;
        
    }
    
}
