import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { urltable } from 'src/app/models/urltable';

import { UrltableService } from 'src/app/services/urltable.service';

@Component({
  selector: 'app-detailview',
  templateUrl: './detailview.component.html',
  styleUrls: ['./detailview.component.css']
})
export class DetailviewComponent implements OnInit {
  @Output() urlUpd = new EventEmitter<urltable[]>();
  urlEdit: urltable = new urltable();
  constructor(private router:Router, private url:UrltableService){}
  URLs: urltable[] = [];
  ngOnInit(): void {
   
  }
  tableMode: boolean = false;
  getUrl(url: urltable){
    this.url.getUrlByCode(url).subscribe(table => {
      console.log('Response', table)
      this.URLs = table
      this.tableMode = true;
    })
  }
}
