import { Component, HostListener } from '@angular/core';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';

@Component({
  selector: 'app-nav-layout',
  standalone: true,
  imports: [RouterLink, RouterLinkActive],
  templateUrl: './nav-layout.html',
  styleUrl: './nav-layout.css',
})
export class NavLayout {
  displayName: string = '';
  activeSection: string = 'hero';

  constructor(private router: Router) {
    this.displayName = localStorage.getItem('displayName') || 'User';
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('userName');
    localStorage.removeItem('displayName');
    localStorage.removeItem('email');
    localStorage.removeItem('userId');
    this.router.navigate(['/login']);
  }

  scrollTo(sectionId: string) {
    this.activeSection = sectionId;
    const el = document.getElementById(sectionId);
    if (el) {
      el.scrollIntoView({
        behavior: 'smooth',
        block: 'start',
      });
    }
  }

  @HostListener('window:scroll', [])
  onWindowScroll() {
    const sections = ['hero', 'destinations', 'my-tickets'];

    for (const section of sections) {
      const el = document.getElementById(section);
      if (el) {
        const rect = el.getBoundingClientRect();
        if (rect.top <= 120 && rect.bottom >= 120) {
          this.activeSection = section;
        }
      }
    }
  }
}
