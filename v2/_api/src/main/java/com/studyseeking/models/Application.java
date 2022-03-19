package com.studyseeking.models;

import lombok.*;

import javax.persistence.*;
import java.util.Set;

@Entity
@Table(name = "applications")
@AllArgsConstructor
@NoArgsConstructor
@ToString
@Getter
@Setter
public class Application {
    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    @Column(name = "id", nullable = false)
    private Integer id;

    @Column(name = "name", nullable = false)
    private String name;

    @Column(name = "subdomain", nullable = false)
    private String subdomain;

    @OneToMany(mappedBy = "application", cascade = CascadeType.ALL)
    private Set<Category> categories;
}
