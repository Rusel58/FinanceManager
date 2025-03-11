using Accounting_for_finance.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting_for_finance.application
{
    /// <summary>
    /// Фасад для работы с банковскими счетами.
    /// </summary>
    public class BankAccountFacade
    {
        // In-memory хранилище счетов.
        private readonly Dictionary<Guid, BankAccount> _bankAccounts = new Dictionary<Guid, BankAccount>();

        /// <summary>
        /// Создание нового банковского счета.
        /// </summary>
        public BankAccount CreateBankAccount(string name, decimal initialBalance)
        {
            BankAccount account = DomainFactory.CreateBankAccount(name, initialBalance);
            _bankAccounts[account.Id] = account;
            return account;
        }

        public BankAccount CreateBankAccount(Guid id, string name, decimal initialBalance)
        {
            BankAccount account = DomainFactory.CreateBankAccount(id, name, initialBalance);
            _bankAccounts[account.Id] = account;
            return account;
        }

        /// <summary>
        /// Получение счета по идентификатору.
        /// </summary>
        public BankAccount GetBankAccount(Guid id)
        {
            if (_bankAccounts.TryGetValue(id, out var account))
                return account;
            throw new Exception("Счет не найден.");
        }

        /// <summary>
        /// Получение всех счетов.
        /// </summary>
        public IEnumerable<BankAccount> GetAllBankAccounts()
        {
            return _bankAccounts.Values;
        }

        /// <summary>
        /// Удаление счета по идентификатору.
        /// </summary>
        public void DeleteBankAccount(Guid id)
        {
            if (!_bankAccounts.ContainsKey(id))
                throw new Exception("Счет не найден.");
            _bankAccounts.Remove(id);
        }

        /// <summary>
        /// Обновление имени счета.
        /// Поскольку свойство Name в BankAccount только для чтения, создаётся новый экземпляр с сохранением идентификатора и баланса.
        /// </summary>
        public BankAccount UpdateBankAccountName(Guid id, string newName)
        {
            if (!_bankAccounts.ContainsKey(id))
                throw new Exception("Счет не найден.");

            BankAccount account = _bankAccounts[id];
            BankAccount updatedAccount = new BankAccount(account.Id, newName, account.Balance);
            _bankAccounts[id] = updatedAccount;
            return updatedAccount;
        }
    }

}
