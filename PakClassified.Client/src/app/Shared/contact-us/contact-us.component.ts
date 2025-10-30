import { Component, inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { LoadingService } from '../../Core/Common/Loading/loading.service';
import emailjs from '@emailjs/browser';

@Component({
  selector: 'app-contact-us',
  imports: [FormsModule, CommonModule],
  templateUrl: './contact-us.component.html',
  styleUrl: './contact-us.component.css'
})
export class ContactUsComponent implements OnInit {
  loadingservice = inject(LoadingService);
  
  formData = {
    name: '',
    email: '',
    subject: '',
    message: ''
  };

  isLoading = false;
  successMessage = '';
  errorMessage = '';

  private readonly emailConfig = {
    serviceId: 'service_p3vrlgn',
    templateId: 'template_hh8si87',
    publicKey: 'Fc1AUiN3oWZCSG4wf'
  };

  ngOnInit(): void {
    window.scrollTo({ top: 0, behavior: 'smooth' });
    this.loadingservice.show();

    setTimeout(() => {
      this.loadingservice.hide();
    }, 1500);

    emailjs.init(this.emailConfig.publicKey);
  }

  async onSubmit() {
    if (this.isLoading) return;

    this.isLoading = true;
    this.successMessage = '';
    this.errorMessage = ''; 

    try {
      const templateParams = {
        from_name: this.formData.name,
        from_email: this.formData.email,
        subject: this.formData.subject,
        message: this.formData.message,
        to_name: 'Jaleel',
        reply_to: this.formData.email,
        date_time: new Date().toLocaleString('en-US', {
          year: 'numeric',
          month: 'long',
          day: 'numeric',
          hour: '2-digit',
          minute: '2-digit',
          timeZoneName: 'short'
        }),
        website_name: 'Jaleel.dev',
        website_url: 'https://jaleel.dev'
      };

      const response = await emailjs.send(
        this.emailConfig.serviceId,
        this.emailConfig.templateId,
        templateParams,
        this.emailConfig.publicKey
      );

      if (response.status === 200) {
        this.successMessage = 'Thank you! Your message has been sent successfully. We will get back to you within 24 hours.';
        this.resetForm();
      } else {
        throw new Error('Failed to send message');
      }

    } catch (error) {
      console.error('EmailJS Error:', error);
      this.errorMessage = 'Sorry, there was an error sending your message. Please try again later or contact us directly at Jaleel@developer.com.';
    } finally {
      this.isLoading = false;
    }
  }

  private resetForm() {
    this.formData = {
      name: '',
      email: '',
      subject: '',
      message: ''
    };
  }
}