using UnityEngine;

public class Shop : MonoBehaviour
{
    BuildManager buildManager;
    public TurretBlueprint standardTurret;
    public TurretBlueprint missileLauncher;
    public TurretBlueprint lazerBeamer;

    
     void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void SelectStandartTurret()
    {
        Debug.Log("Standart Turret Purchased");
        buildManager.SelectTurretBuild(standardTurret);
    }
    
    public void SelectMissileLauncher()
    {
        Debug.Log("Missile Launcher  Purchased");
        buildManager.SelectTurretBuild(missileLauncher);
    }

    public void SelectLazerBeamer()
    {
        Debug.Log("Lazer Beamer  Purchased");
        buildManager.SelectTurretBuild(lazerBeamer);
    }

}
