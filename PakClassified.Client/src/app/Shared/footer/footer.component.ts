import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import emailjs, { EmailJSResponseStatus } from '@emailjs/browser';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-footer',
  imports: [FormsModule, CommonModule, RouterLink],
  templateUrl: './footer.component.html',
  styleUrl: './footer.component.css'
})
export class FooterComponent {
  email: string = '';
  isLoading: boolean = false;
  message: string = '';
  messageType: 'success' | 'error' | '' = '';

  private readonly PUBLIC_KEY = 'Fc1AUiN3oWZCSG4wf';
  private readonly SERVICE_ID = 'service_p3vrlgn';
  private readonly TEMPLATE_ID = 'template_tfiwjwg';

  constructor() {
    emailjs.init(this.PUBLIC_KEY);
  }

  async onSubscribe() {
    if (!this.email || !this.isValidEmail(this.email)) {
      this.showMessage('Please enter a valid email address.', 'error');
      return;
    }

    this.isLoading = true;
    this.message = '';

    try {
      const templateParams = {
        subscriber_email: this.email,
        from_name: 'Pak Classified'
      };

      const response: EmailJSResponseStatus = await emailjs.send(
        this.SERVICE_ID,
        this.TEMPLATE_ID,
        templateParams
      );

      if (response.status === 200) {
        this.showMessage('Thank you for subscribing! Check your email for confirmation.', 'success');
        this.email = ''; 
      }
    } catch (error) {
      console.error('EmailJS Error:', error);
      this.showMessage('Failed to send confirmation email. Please try again.', 'error');
    } finally {
      this.isLoading = false;
    }
  }

  private isValidEmail(email: string): boolean {
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return emailRegex.test(email);
  }

  private showMessage(text: string, type: 'success' | 'error') {
    this.message = text;
    this.messageType = type;
    
    setTimeout(() => {
      this.message = '';
      this.messageType = '';
    }, 5000);
  }
}