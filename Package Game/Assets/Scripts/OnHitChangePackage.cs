using UnityEngine;
using System.Collections;

public class OnHitChangePackage : MonoBehaviour
{
    private ParticleSystem PackageParticle;

    [SerializeField] private Gradient[] ParticleGradient;

    private float TestTimer;

    private void Awake()
    {
        PackageParticle = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        TestTimer += Time.deltaTime;

        if (TestTimer > 1 && transform.name == "Cooldown1")
        {
            transform.name = "Package1(Clone)";
            TestTimer = 0;
        }

        if (TestTimer > 1 && transform.name == "Cooldown2")
        {
            transform.name = "Package2(Clone)";
            TestTimer = 0;
        }

        if (TestTimer > 1 && transform.name == "Cooldown3")
        {
            transform.name = "Package3(Clone)";
            TestTimer = 0;
        }
    }

    public void AddStamp()
    {
        transform.GetChild(1).gameObject.SetActive(true);
        ParticleSystem.MainModule ParSysMain = PackageParticle.main;
        ParSysMain.startColor = ParticleGradient[0];
        PackageParticle.Play();
    }

    public void PlayNegativeParticle()
    {
        ParticleSystem.MainModule ParSysMain = PackageParticle.main;
        ParSysMain.startColor = ParticleGradient[1];
        ParSysMain.maxParticles = 200;
        ParSysMain.startSize = 0.15f;
        ParSysMain.startLifetime = 0.25f;
        PackageParticle.Play();
    }

    IEnumerator ChangeName()
    {
        yield return new WaitForSeconds(4);
        print("BAKFIETS");
    }
}