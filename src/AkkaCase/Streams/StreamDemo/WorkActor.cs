using Akka.Actor;
using Microsoft.VisualBasic;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamDemo;

public class WorkActor : ReceiveActor
{
    private readonly ILogger _logger = Log.Logger.ForContext<WorkActor>();
    public WorkActor()
    {

        Receive<string>(msg =>
        {
            _logger.Debug(msg);
        });
        Receive<int>(msg =>
        {
            _logger.Debug(msg.ToString());
        });
        Receive((Action<string>)(msg =>
        {
            _logger.Debug("Action:" + msg);
        }));
        Receive<int>(msg =>
        {
            _logger.Debug("second int:" + msg.ToString());
        });
    }
}
