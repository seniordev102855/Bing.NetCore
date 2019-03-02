﻿using System.ComponentModel.DataAnnotations;
using Bing.Biz.OAuthLogin.Core;

namespace Bing.Biz.OAuthLogin.Github.Configs
{
    /// <summary>
    /// Github授权配置
    /// </summary>
    public class GithubAuthorizationConfig:AuthorizationConfigBase
    {
        /// <summary>
        /// 应用标识
        /// </summary>
        [Required(ErrorMessage = "应用标识[AppId]不能为空")]
        public string AppId { get; set; }

        /// <summary>
        /// 应用密钥
        /// </summary>
        [Required(ErrorMessage = "应用密钥[AppKey]不能为空")]
        public string AppKey { get; set; }

        /// <summary>
        /// 回调地址
        /// </summary>
        [Required(ErrorMessage = "回调地址[CallbackUrl]不能为空")]
        public string CallbackUrl { get; set; }

        /// <summary>
        /// 应用名称
        /// </summary>
        [Required(ErrorMessage = "应用名称[ApplicationName]不能为空")]
        public string ApplicationName { get; set; }
    }
}
