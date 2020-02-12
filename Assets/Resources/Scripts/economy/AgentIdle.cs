
using UnityEngine;

internal class AgentIdle : AgentAction
{
    private BaseAgent baseAgent;

    public AgentIdle(BaseAgent baseAgent) {
        this.baseAgent = baseAgent;
    }

    public override void Act() {
        Debug.Log(baseAgent + " performed the " + this.ToString());
    }
}