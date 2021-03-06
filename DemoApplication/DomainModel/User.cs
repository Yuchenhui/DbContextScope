﻿using System;

namespace Numero3.EntityFramework.Demo.DomainModel
{
	// Anemic model to keep this demo application simple.
	public class User
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public int CreditScore { get; set; }
		public bool WelcomeEmailSent { get; set; }
		public DateTime CreatedOn { get; set; }

		public override string ToString()
		{
			return
			    $"Id: {Id} | Name: {Name} | Email: {Email} | CreditScore: {CreditScore} | WelcomeEmailSent: {WelcomeEmailSent} | CreatedOn (UTC): {CreatedOn.ToString("dd MMM yyyy - HH:mm:ss")}";
		}
	}

    // Anemic model to keep this demo application simple.
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return
                $"Id: {Id} | Name: {Name}";
        }
    }
}
