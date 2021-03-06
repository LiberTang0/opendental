using System;
using System.Collections;

namespace OpenDentBusiness{

	///<summary>A template email which can be used as the basis for a new email.</summary>
	[Serializable]
	public class EmailTemplate:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long EmailTemplateNum;
		///<summary>Default subject line.</summary>
		public string Subject;
		///<summary>Body of the email</summary>
		public string BodyText;
		///<summary>Different than Subject.  The description of the email template.  This is what the user sees in the list.</summary>
		public string Description;
		
		///<summary>Returns a copy of this EmailTemplate.</summary>
		public EmailTemplate Copy(){
			return (EmailTemplate)this.MemberwiseClone();
		}

		

		
		
	}

	
	

}













