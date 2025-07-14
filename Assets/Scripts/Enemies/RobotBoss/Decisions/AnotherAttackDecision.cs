using UnityEngine;
[CreateAssetMenu(menuName = "PluggableAI/Decisions/AnotherAttackDecision")]
public class AnotherAttackDecision : Decision
{


    [SerializeField] int inclusiceRangeMin;
    [SerializeField] int inclusiceRangeMax;
    public override bool Decide(StateController controller)
    {

        if (controller.readyToGoNextState) {
            int randomValue = Random.Range(inclusiceRangeMin, inclusiceRangeMax + 1);

            if (randomValue == 1)
            {
                return true;

            }
            else return false;




        }

      return false;
    }
}
