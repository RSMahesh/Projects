using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IMM = InfoWebTicketSystem.MVC.ViewModels;


namespace InfoWebTicketSystem.MVC
{
    public static class AutoMapper
    {
        public static void Map()
        {
            Mapper.CreateMap<IMM.RegisterViewModel, Entities.Account>();

            Mapper.CreateMap<IMM.LoginViewModel, Entities.Account>().ForMember(des => des.Email,
                m => m.MapFrom(sour => sour.UserName)).ForMember(des => des.Email,
                m => m.MapFrom(sour => sour.UserName));

            Mapper.CreateMap<IMM.CollectTicketDetailViewModal, Entities.Ticket>().ForMember(
                des => des.TicketType, m => m.MapFrom(sour => sour.TicketType)).ForMember(
                 des => des.UserCommunicationEmail, m => m.MapFrom(sour => sour.UserEmail)).ForMember(
                des => des.Domain, m => m.MapFrom(sour => sour.Domain)).ForMember(
                des => des.Subject, m => m.MapFrom(sour => sour.Subject)).ForMember(
                des => des.Subject, m => m.MapFrom(sour => sour.Subject)).AfterMap((sour, dest) =>
           {
               var tickConv = new Entities.TicketConversation();
               tickConv.Attachment = sour.Attachment;
               tickConv.Message = sour.Message;
               dest.TicketConversations.Add(tickConv);

           });

            Mapper.CreateMap<Entities.Ticket, IMM.Ticket>().ForMember(des => des.TicketNumber,
               m => m.MapFrom(sour => "#NMU-" + sour.TicketNumber.ToString().PadLeft(5, '0') + "-XOJ")).ForMember(
               des => des.Type, m => m.MapFrom(sour => sour.TicketType == "NI" ? "Need Information" : "Error On WebSite"));

            Mapper.CreateMap<Entities.Ticket, IMM.ViewTicketViewModel>();

            Mapper.CreateMap<IMM.ViewTicketViewModel, Entities.Ticket>();

            Mapper.CreateMap<Entities.Ticket, IMM.SubmitConfirmationViewModal>().ForMember(
                des => des.UserEmail, m => m.MapFrom(sour => sour.UserCommunicationEmail)).AfterMap((sour, dest) =>
                {
                    if (sour.User != null)
                    {
                       // dest.UserEmail = sour.User.Email;
                        dest.UserName = sour.User.UserName;
                    }
                });


        }

    }
}