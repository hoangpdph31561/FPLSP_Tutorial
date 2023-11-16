namespace FPLSP_Tutorial.WASM.Response
{
    public abstract class ViewModelBase<TDataType> : APIResponse
    {
        public abstract Task HandleAsync(TDataType data, CancellationToken cancellationToken);
    }
}
