import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Photo } from 'src/app/_models/photo';
import { FileUploader } from 'ng2-file-upload';
import { environment } from 'src/environments/environment';
import { AuthService } from 'src/app/_services/auth.service';
import { UserService } from 'src/app/_services/user.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { User } from 'src/app/_models/user';
import { ActivatedRoute } from '@angular/router';


@Component({
  selector: 'app-photo-editor',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.css']
})
export class PhotoEditorComponent implements OnInit {
  @Input() photos: Photo[]
  @Output() getMemberPhotoChange = new EventEmitter<string>();
  uploader: FileUploader;
  hasBaseDropZoneOver = false;
  baseUrl = environment.apiUrl;
  currentMain :Photo;
  user:User;


  constructor(private authService: AuthService , private route:ActivatedRoute,private userService:UserService,
    private alertify:AlertifyService
    ) { }

  ngOnInit() {
    this.initializeUploader();
    // this.route.data.subscribe(data => {
    //   this.user = data['user'];
    // })
     }

  fileOverBase(e: any): void {
    this.hasBaseDropZoneOver = e;
  }

  initializeUploader() {
    this.uploader = new FileUploader(
      {
        url: this.baseUrl + 'users/' + this.authService.decodedToken.nameid + '/photos',
        authToken: 'Bearer ' + localStorage.getItem('token'),
        isHTML5: true,
        allowedFileType: ['image'],
        removeAfterUpload: true,
        autoUpload: false,
        maxFileSize: 10 * 1024 * 1024,
        
      }
    );
    this.uploader.onAfterAddingFile=(file)=>{file.withCredentials=false;};
    this.uploader.onSuccessItem=(item,Response,status,headers)=>{
      if(Response){
        const res:Photo = JSON.parse(Response);
        const photo ={
          id:res.id,
          url:res.url,
          dateAdded:res.dateAdded,
          isMain:res.isMain,
          isApproved:res.isApproved
        };
        this.photos.push(photo);
        if(photo.isMain){
      this.authService.changeMemberPhoto(photo.url);
      this.authService.currentUser.photoURL=photo.url;
      localStorage.setItem('user',JSON.stringify(this.authService.currentUser));
        }
      }
    }
  }

  setMainPhoto(photo:Photo){
    this.userService.setMainPhoto(this.authService.decodedToken.nameid,photo.id).subscribe(
      ()=>{this.currentMain=this.photos.filter(p=>p.isMain===true)[0];
      this.currentMain.isMain=false;
      photo.isMain=true;
      // this.getMemberPhotoChange.emit(photo.url);
      // this.user.photoURL= photo.url;
      this.authService.changeMemberPhoto(photo.url);
      this.authService.currentUser.photoURL=photo.url;
      localStorage.setItem('user',JSON.stringify(this.authService.currentUser));
      
      },
      ()=>{this.alertify.error('يوجد مشكلة في الصورة الأساسية');}
    )
  }

  delete(id:number){
    this.alertify.confirm("هل تريد حذف تلك الصورة",()=>{
      this.userService.deletePhoto(this.authService.decodedToken.nameid,id).subscribe(
        ()=>{
          this.photos.splice(this.photos.findIndex(p=>p.id===id),1);
          this.alertify.success("تم حذف الصورة بنجاح");
        },
        error=>{this.alertify.error("حدث خطأ أثناء حذف الصورة");}

      );
    });
  }

}
