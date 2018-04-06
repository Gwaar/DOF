using system;
using system.collections;
using system.collections.generic;
using unityengine;

[createassetmenu(menuname = "pluggabeai/state ")]


public class state : scriptableobject
{
    public action[] actions;
    public transition[] transitions;
    public color scenegizmocolor = color.gray;

    public void updatestate(statecontroller controller)
    {
        doactions(controller);
        checktransitions(controller);
    }

    private static void doactions(statecontroller controller)
    {
        doactions(controller);
    }


    private void checktransitions(statecontroller controller)
    {
        for (int i = 0; i < transitions.length; i++)
        {
            bool desitionsucceeded = transitions[i].decision.decide(controller);
            if (desitionsucceeded)
            {
                controller.transitiontostate(transitions[i].truestate);
            }
            else
            {
                controller.transitiontostate(transitions[i].falsestate);
            }
        }
    }

}

