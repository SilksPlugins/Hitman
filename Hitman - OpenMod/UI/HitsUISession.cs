﻿using Cysharp.Threading.Tasks;
using Hitman.API.UI;
using Hitman.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using OpenMod.API.Permissions;
using OpenMod.Core.Permissions.Events;
using OpenMod.Unturned.Users;
using SilK.Unturned.Extras.Configuration;
using SilK.Unturned.Extras.Events;
using SilK.Unturned.Extras.UI;
using System;

namespace Hitman.UI
{
    public class HitsUISession : SingleEffectUISession,
        IInstanceAsyncEventListener<IHitListUpdatedEvent>,
        IInstanceAsyncEventListener<PermissionActorRoleEvent>
    {
        public override string Id => "Hitman.Hits";

        public override ushort EffectId => _configuration.Instance.UI.EffectId;

        private readonly IConfigurationParser<HitmanConfiguration> _configuration;
        private readonly IHitsUIManager _hitsUIManager;
        private readonly IStringLocalizer _stringLocalizer;
        private readonly IPermissionChecker _permissionChecker;
        private readonly ILogger<HitsUISession> _logger;

        public const int MaxPossibleElements = 10;

        public HitsUISession(
            IConfigurationParser<HitmanConfiguration> configuration,
            IHitsUIManager hitsUIManager,
            IStringLocalizer stringLocalizer,
            IPermissionChecker permissionChecker,
            ILogger<HitsUISession> logger,
            UnturnedUser user,
            IServiceProvider serviceProvider) : base(user, serviceProvider)
        {
            _configuration = configuration;
            _hitsUIManager = hitsUIManager;
            _stringLocalizer = stringLocalizer;
            _permissionChecker = permissionChecker;
            _logger = logger;
        }

        protected override async UniTask OnStartAsync()
        {
            await UniTask.SwitchToMainThread();

            SendUIEffect(_stringLocalizer["ui:header"]);

            await UpdateHitsAsync();
        }

        protected override async UniTask OnEndAsync()
        {
            await UniTask.SwitchToMainThread();
            
            SendVisibility("CloseTrigger", false);

            await UniTask.Delay(2000);

            ClearEffect();
        }

        private async UniTask UpdateHitsAsync()
        {
            await UniTask.SwitchToMainThread();

            var i = 0;

            var topHits = _hitsUIManager.GetTopHits();

            foreach (var entry in topHits)
            {
                SendText($"HitName ({i})", entry.Name);
                SendText($"HitPrice ({i})", entry.Bounty);
                if (entry.Icon != null) SendImage($"HitIcon ({i})", entry.Icon);
                SendVisibility($"Hit ({i})", true);

                i++;
            }

            for (; i < MaxPossibleElements; i++)
            {
                SendVisibility($"Hit ({i})", false);
            }
        }

        public async UniTask HandleEventAsync(object? sender, IHitListUpdatedEvent @event)
        {
            await UpdateHitsAsync();
        }

        public UniTask HandleEventAsync(object? sender, PermissionActorRoleEvent @event)
        {
            if (@event.Actor.Type == User.Type || @event.Actor.Id == User.Id)
            {
                UniTask.RunOnThreadPool(async () =>
                {
                    if (await _permissionChecker.CheckPermissionAsync(User, "ui") != PermissionGrantResult.Grant)
                    {
                        await EndAsync();
                    }
                }).Forget();
            }

            return UniTask.CompletedTask;
        }
    }
}
