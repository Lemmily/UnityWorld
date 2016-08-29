
using UnityEngine;

internal class AgentIdle : AgentAction
{
    private Agent agent;

    public AgentIdle(Agent agent) {
        this.agent = agent;
    }

    public override void Act() {
        Debug.Log(agent + " performed the " + this.ToString());
    }
}