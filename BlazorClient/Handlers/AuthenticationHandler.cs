using Blazored.LocalStorage;

namespace BlazorClient.Handlers
{

	public class AuthenticationHandler : DelegatingHandler
	{
		protected ILocalStorageService LocalStorage
		{
			get; set;
		}

		public AuthenticationHandler(ILocalStorageService localStorage)
		{
			LocalStorage = localStorage;
		}

		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			var token = await LocalStorage.GetItemAsync<string>("token", cancellationToken);
			var uid = await LocalStorage.GetItemAsync<string>("uid", cancellationToken);

			if (!string.IsNullOrEmpty(token))
			{
				request.Headers.Add("Authorization", $"Bearer {token}");
			}

			if (!string.IsNullOrEmpty(uid))
			{
				request.Headers.Add("Uid", uid);
			}

			return await base.SendAsync(request, cancellationToken);
		}
	}

}
