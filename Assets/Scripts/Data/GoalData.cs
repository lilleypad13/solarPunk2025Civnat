using UnityEngine;

[CreateAssetMenu(fileName = "GoalData", menuName = "Scriptable Objects/GoalData")]
public class GoalData : ScriptableObject
{
    [SerializeField] private int goalPollution = 100;
    [SerializeField] private int goalEco = 100;
    [SerializeField] private int goalEnergy = 100;
    [SerializeField] private int goalCommunityHealth = 100;

    public bool IsAtPollutionGoal(int value)
    {
        return value < goalPollution;
    }

    public bool IsAtEcoGoal(int value)
    {
        return value > goalEco;
    }

    public bool IsAtEnergyGoal(int value)
    {
        return value > goalEnergy;
    }

    public bool IsAtCommunityHealthGoal(int value)
    {
        return value > goalCommunityHealth;
    }
}
