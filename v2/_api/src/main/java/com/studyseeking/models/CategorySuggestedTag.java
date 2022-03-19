package com.studyseeking.models;

import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

import javax.persistence.EmbeddedId;
import javax.persistence.Entity;
import javax.persistence.Table;

@Entity
@Table(name = "category_has_suggested_tag")
@Getter
@Setter
@AllArgsConstructor
@NoArgsConstructor
public class CategorySuggestedTag {
    @EmbeddedId
    private CategorySuggestedTagId categorySuggestedTagId;
}
