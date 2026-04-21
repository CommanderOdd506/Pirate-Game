public interface IStasisable
{
    public void BeginStasis();
    public void EndStasis();
    void OnStasisTargeted();
    void OnStasisUntargeted();

    event System.Action<IStasisable> OnDestroyed;
}