import { getLocalePluralCase } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { CanDelete } from 'src/app/models/candeletemodel';
import { urltable } from 'src/app/models/urltable';
import { User } from 'src/app/models/user';
import { AuthService } from 'src/app/services/auth.service';
import { UrltableService } from 'src/app/services/urltable.service';
import { UserStoreService } from 'src/app/services/user-store.service';


@Component({
  selector: 'app-urltable',
  templateUrl: './urltable.component.html',
  styleUrls: ['./urltable.component.css']
})
export class URLtableComponent implements OnInit{
    title = 'Table';
    @Input() url?: urltable;
    tableMode: boolean = true;
    @Output() urlUpd = new EventEmitter<urltable[]>();

    urlEdit: urltable = new urltable();
    user!: User;
    getUserNameFromToken: string ='';
    getRoleFromToken: string = '';
    URLs: urltable[] =[];
    isVisible:any;
    deletable:any;
    canDelete: CanDelete[] = [];
    constructor(private urltableservise: UrltableService, private userStor: UserStoreService,private auth:AuthService){}
    ngOnInit(): void {
      if (this.auth.isLogged()) {
        this.userStor.getUserName()
          .subscribe(val => {
            this.getUserNameFromToken = this.auth.getUserNameFromToken();
          });

        this.userStor.getRoles()
          .subscribe(val => {
            this.getRoleFromToken = this.auth.getUserRoleFromToken();
          });

        this.isVisible = true;
      };
      this.urltableservise.getUrls().subscribe(table =>{
        console.log('Response', table)
        this.URLs = table;
        if(this.getRoleFromToken == 'User'){
        for (let u of this.URLs){
          if (u.createdBy == this.getUserNameFromToken) {
            let cd = new CanDelete();
            cd.id = u.id;
            cd.canDelete = true;
            this.canDelete.push(cd);
          }
          else{
            let cd = new CanDelete();
            cd.id = u.id;
            cd.canDelete = false;
            this.canDelete.push(cd);  
          }
        }       
        } 
        else{
          for (let u of this.URLs){
            let cd = new CanDelete();
            cd.id = u.id;
            cd.canDelete = true;
            this.canDelete.push(cd);
          }
        }
      }); 
    }
    refresh(): void {
      this.auth.refresh()
    }
    deleteUrl(url: urltable){
      this.urltableservise.deleteUrls(url).subscribe((URLs: urltable[]) => this.urlUpd.emit(URLs)) ; 
      this.refresh();
    }
    createUrl(url: urltable){
      this.urlEdit.createdBy = this.getUserNameFromToken;
      this.urltableservise.createUrls(url).subscribe((URLs: urltable[]) => this.urlUpd.emit(URLs));
      this.refresh();
    }
    updateUrl(url: urltable) {
      this.urltableservise.updateUrls(url).subscribe((URLs: urltable[]) => this.urlUpd.emit(URLs));
      this.refresh();
    }
    initNewUrl(){
      this.cancel();
      this.tableMode = false;
    }
    editUrl(url:urltable){
      this.urlEdit =url;
    }
    cancel() {
    this.urlEdit = new urltable();
    this.tableMode = true;
    }
}
