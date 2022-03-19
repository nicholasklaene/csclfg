package com.studyseeking.models;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.Id;
import javax.persistence.Table;

@Entity
@Table(name = "tags")
public class Tag {
    @Id
    @Column(name = "label", nullable = false)
    private String label;
}
