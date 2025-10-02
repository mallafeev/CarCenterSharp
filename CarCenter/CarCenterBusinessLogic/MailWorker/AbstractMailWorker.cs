﻿using CarCenterContracts.BindingModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCenterBusinessLogic.MailWorker
{
    public abstract class AbstractMailWorker
    {
        protected string _mailLogin = string.Empty;
        protected string _mailPassword = string.Empty;
        protected string _smtpClientHost = string.Empty;
        protected int _smtpClientPort;
        protected string _popHost = string.Empty;
        protected int _popPort;
        private readonly ILogger _logger;
        public AbstractMailWorker(ILogger<AbstractMailWorker> logger)
        {
            _logger = logger;
        }
        public void MailConfig(MailConfigModel config)
        {
            _mailLogin = config.MailLogin;
            _mailPassword = config.MailPassword;
            _smtpClientHost = config.SmtpClientHost;
            _smtpClientPort = config.SmtpClientPort;
            _popHost = config.PopHost;
            _popPort = config.PopPort;
            _logger.LogDebug("Config: {login}, {password}, {clientHost}, {clientPort}, {popHost}, {popPort} ", _mailLogin, _mailPassword, _smtpClientHost, _smtpClientPort, _popHost, _popPort);
        }
        public async void MailSendAsync(MailSendInfoModel info)
        {
            if (string.IsNullOrEmpty(_mailLogin) || string.IsNullOrEmpty(_mailPassword))
            {
                return;
            }
            if (string.IsNullOrEmpty(_smtpClientHost) || _smtpClientPort == 0)
            {
                return;
            }
            if (string.IsNullOrEmpty(info.MailAddress) || string.IsNullOrEmpty(info.Subject) || (string.IsNullOrEmpty(info.Text) && info.Pdf == null))
            {
                return;
            }
            _logger.LogDebug("Send Mail: {To}, {Subject}", info.MailAddress, info.Subject);
            await SendMailAsync(info);
        }
        protected abstract Task SendMailAsync(MailSendInfoModel info);
    }
}
