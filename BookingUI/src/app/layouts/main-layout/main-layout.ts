import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavLayout } from '../nav-layout/nav-layout';
import { FooterLayout } from '../footer-layout/footer-layout';
import { Alert } from '../alert/alert';
@Component({
  selector: 'app-main-layout',
  imports: [RouterOutlet,NavLayout,FooterLayout,Alert],
  templateUrl: './main-layout.html',
  styleUrl: './main-layout.css',
})
export class MainLayout {}
