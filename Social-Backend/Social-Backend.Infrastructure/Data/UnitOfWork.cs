using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Social_Backend.Core.Interfaces;
using Social_Backend.Core.Interfaces.Chat;
using Social_Backend.Core.Interfaces.ChatRole;
using Social_Backend.Core.Interfaces.Message;
using Social_Backend.Core.Interfaces.User;
using Social_Backend.Core.Interfaces.UserChat;
using Social_Backend.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Social_Backend.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool _disposed;
        private string _errorMessage = string.Empty;
        private IDbContextTransaction _objTran;
        public SocialDBContext Context { get; }
        public IChatRepository ChatRepository { get; set; }
        public IChatRoleRepository ChatRoleRepository { get; set; }
        public IMessageRepository MessageRepository { get; set; }
        public IUserChatRepository UserChatRepository { get; set; }
        public IUserRepository UserRepository { get; set; }

        public UnitOfWork(IChatRepository chatRepository, IChatRoleRepository chatRoleRepository,
            IMessageRepository messageRepository, IUserChatRepository userChatRepository, IUserRepository userRepository, SocialDBContext context)
        {
            Context = context;
            ChatRepository = chatRepository;
            ChatRoleRepository = chatRoleRepository;
            MessageRepository = messageRepository;
            UserChatRepository = userChatRepository;
            UserRepository = userRepository;
        }

        public async Task Commit()
        {
            await _objTran.CommitAsync();
        }

        public async Task CreateTransaction()
        {
            _objTran = await Context.Database.BeginTransactionAsync();
        }

        public async Task Rollback()
        {
            await _objTran.RollbackAsync();
            await _objTran.DisposeAsync();
        }

        public async Task Save()
        {
            try
            {
                await Context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw new Exception("Error while executing this operation");
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    Context.Dispose();
            _disposed = true;
        }
    }
}