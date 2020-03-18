using System;

namespace EventBus.Events
{
  public class CarrierCreateFailed : IFailedEvent
  {
    public Guid CarrierId { get; set; }
    public string Reason { get; }

    public string Code { get; }

    public CarrierCreateFailed(Guid carrierId, string reason, string code)
    {
      CarrierId = carrierId;
      Reason = reason;
      Code = code;
    }
  }
}