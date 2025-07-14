import { TextFieldModule } from '@angular/cdk/text-field';
import { ChangeDetectionStrategy, Component, model } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-message-field',
  imports: [MatFormFieldModule, MatInputModule, TextFieldModule, MatButtonModule, MatIconModule, FormsModule],
  templateUrl: './message-field.html',
  styleUrl: './message-field.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MessageField {
  protected text = model('')
}
