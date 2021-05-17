import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CoctailBuilderComponent } from './content/coctail-builder/coctail-builder.component';
import { DrinksComponent } from './content/drinks/drinks.component';
import { GameComponent } from './content/game/game.component';
import { CalculatorComponent } from './content/calculator/calculator.component';
import { RegistrationComponent } from './account/registration/registration.component';
import { LoginComponent } from './account/login/login.component';
import { DrinkComponent } from './content/drink/drink.component';
import { AdminPanelComponent } from './account/admin-panel/admin-panel.component';
import { UserProfilComponent } from './account/user-profil/user-profil.component';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './_guards/auth.guard';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'registration', component: RegistrationComponent },
  { path: 'login', component: LoginComponent },
  { path: 'drinks', component: DrinksComponent },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      { path: 'coctailBuilder', component: CoctailBuilderComponent },
      { path: 'game', component: GameComponent },
      { path: 'admin-panel', component: AdminPanelComponent },
      { path: 'account', component: UserProfilComponent },
      { path: 'calculator', component: CalculatorComponent },
      { path: 'drink/:id', component: DrinkComponent },
      { path: 'search/:keyword', component: DrinksComponent }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
