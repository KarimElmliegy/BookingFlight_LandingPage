import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavLayout } from '../nav-layout/nav-layout';
import { FooterLayout } from '../footer-layout/footer-layout';
@Component({
  selector: 'app-main-layout',
  imports: [RouterOutlet,NavLayout,FooterLayout],
  templateUrl: './main-layout.html',
  styleUrl: './main-layout.css',
})
export class MainLayout {}
