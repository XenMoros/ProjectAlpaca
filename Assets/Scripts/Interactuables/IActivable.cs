
// Interfaz que marca los objetos Activables por botones o palancas
public interface IActivable
{
    void SetActivationState(bool activateState);
    void SetActivationState();
    void SetActivationState(int activateState);

}