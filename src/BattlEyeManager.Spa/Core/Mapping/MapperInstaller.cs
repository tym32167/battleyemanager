using AutoMapper;
using BattlEyeManager.BE.Models;
using BattlEyeManager.BE.Services;
using BattlEyeManager.DataLayer.Models;
using BattlEyeManager.DataLayer.Repositories;
using BattlEyeManager.Spa.Api.Sync;
using BattlEyeManager.Spa.Model;
using Microsoft.Extensions.DependencyInjection;
using System;
using Player = BattlEyeManager.DataLayer.Models.Player;

namespace BattlEyeManager.Spa.Core.Mapping
{
    public static class MapperInstaller
    {
        public static void AddInternalMapper(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IMapper>(new InternalMapper(SetupMappings()));
        }


        private static AutoMapper.IMapper SetupMappings()
        {
            var config = new MapperConfiguration(configurationExpression =>
            {
                configurationExpression.CreateMap<Server, ServerModel>();
                configurationExpression.CreateMap<Server, ServerSimpleModel>();
                configurationExpression.CreateMap<ServerModel, Server>();

                configurationExpression.CreateMap<KickReason, KickReasonModel>();
                configurationExpression.CreateMap<KickReasonModel, KickReason>();

                configurationExpression.CreateMap<BanReason, BanReasonModel>();
                configurationExpression.CreateMap<BanReasonModel, BanReason>();

                configurationExpression.CreateMap<Server, ServerInfo>();
                configurationExpression.CreateMap<ServerModel, ServerInfo>();
                configurationExpression.CreateMap<ServerModel, ServerInfoDto>();

                configurationExpression.CreateMap<ServerScript, ServerScriptModel>();
                configurationExpression.CreateMap<ServerScriptModel, ServerScript>();

                configurationExpression.CreateMap<Server, OnlineServerModel>();

                configurationExpression.CreateMap<Ban, OnlineBanViewModel>();

                configurationExpression
                    .CreateMap<Player, OnlinePlayerModel>(MemberList.None)
                    .ForMember(x => x.Country, opt => opt.Ignore());

                configurationExpression
                    .CreateMap<BattlEyeManager.BE.Models.Player, OnlinePlayerModel>(MemberList.None)
                    .ForMember(x => x.Country, opt => opt.Ignore());


                configurationExpression.CreateMap<Player, PlayerSyncDto>();

                configurationExpression.CreateMap<Mission, OnlineMissionModel>();

                configurationExpression.CreateMap<BE.Models.ChatMessage, ChatMessageModel>()
                    .AfterMap((message, messageModel) =>
                    {
                        messageModel.Date = DateTime.SpecifyKind(messageModel.Date, DateTimeKind.Utc);
                    });
            });

            config.CompileMappings();

            return config.CreateMapper();
        }
    }
}