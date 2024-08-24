using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using gmail.Areas.Identity.Data;
using gmail.Services;


public class ForgotPasswordModel : PageModel
{
	private readonly UserManager<gmailUser> _userManager;
	private readonly EmailSender _emailSender;
	public  ForgotPasswordModel(UserManager<gmailUser> userManager, EmailSender emailSender)
	{
		_userManager = userManager;
		_emailSender = emailSender;
	}
	[BindProperty]
	public InputModel Input { get; set; }

	public class InputModel
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }
	}

	public async Task<IActionResult> OnPostAsync()
	{
		if (ModelState.IsValid)
		{
			var user = await _userManager.FindByEmailAsync(Input.Email);
			if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
			{
				// Don't reveal that the user does not exist or is not confirmed
				return RedirectToPage("./ForgotPasswordConfirmation");
			}
			var token = await _userManager.GeneratePasswordResetTokenAsync(user);
			var callbackUrl = Url.Page(
				"/Account/ResetPassword",
				pageHandler: null,
				values: new { area = "Identity", userId = user.Id, code = token },
				protocol: Request.Scheme);
			// Using the EmailSender service to send the email
			await _emailSender.SendEmailAsync(
				Input.Email,
				"Reset Password",
				$"Please reset your password by <a href='{callbackUrl}'>clicking here</a>.");
			return RedirectToPage("./ForgotPasswordConfirmation");
		}
		return Page();
	}
}