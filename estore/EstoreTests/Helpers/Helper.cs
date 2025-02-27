﻿using System;
using System.Transactions;
using Estore.DAL;

namespace EstoreTests.Helpers
{
	static public class Helper
	{
		public static TransactionScope CreateTransactionScope(int seconds = 99999999)
		{
			return new TransactionScope(
                TransactionScopeOption.Required,
				new TimeSpan(0, 0, seconds),
                TransactionScopeAsyncFlowOption.Enabled
                );
		}
    }
}

