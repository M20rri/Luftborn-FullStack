import { Component, OnInit } from '@angular/core';
import { UserService } from '../user.service';
import { UserDto } from '../user.dto';
import { ActivatedRoute, Router } from '@angular/router';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-user-form',
  templateUrl: './user-form.component.html',
  styleUrls: ['./user-form.component.css']
})
export class UserFormComponent implements OnInit {

  user: UserDto = { id: 0, firstName: '', lastName: '', email: '' };

  constructor(
    private userService: UserService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.userService.getUser(+id).subscribe((data) => {
        this.user = data;
      });
    }
  }

  saveUser(form: NgForm): void {
    if (form.valid) {

      let model =  {
        "user": this.user
      }


      if (this.user.id) {
        this.userService.updateUser(model).subscribe(() => {
          this.router.navigate(['/']);
        });
      } else {
        this.userService.addUser(model).subscribe(() => {
          this.router.navigate(['/']);
        });
      }
    }
  }
}
