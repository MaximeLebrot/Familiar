public interface IZappable
{
    /// <summary>
    /// If used to keep track of zap state, clear it in LateUpdate().
    /// </summary>
    public abstract bool IsZapped
    {
        get;
        set;
    }

    public abstract void OnZap();
}