namespace BodyLifeSkillsPlatform.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Migrations : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.BehaviourScaleItems",
            //    c => new
            //    {
            //        Id = c.String(nullable: false, maxLength: 128,
            //                annotations: new Dictionary<string, AnnotationValues>
            //                {
            //                    {
            //                        "ServiceTableColumn",
            //                        new AnnotationValues(oldValue: null, newValue: "Id")
            //                    },
            //                }),
            //        BehaviourScale = c.String(),
            //        Name = c.String(),
            //        BehaviourScaleLevel = c.Int(nullable: false),
            //        BehaviourScaleType = c.Int(nullable: false),
            //        Archived = c.Boolean(nullable: false),
            //        Migrated = c.Boolean(nullable: false),
            //        UserID = c.String(),
            //        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion",
            //                annotations: new Dictionary<string, AnnotationValues>
            //                {
            //                    {
            //                        "ServiceTableColumn",
            //                        new AnnotationValues(oldValue: null, newValue: "Version")
            //                    },
            //                }),
            //        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7,
            //                annotations: new Dictionary<string, AnnotationValues>
            //                {
            //                    {
            //                        "ServiceTableColumn",
            //                        new AnnotationValues(oldValue: null, newValue: "CreatedAt")
            //                    },
            //                }),
            //        UpdatedAt = c.DateTimeOffset(precision: 7,
            //                annotations: new Dictionary<string, AnnotationValues>
            //                {
            //                    {
            //                        "ServiceTableColumn",
            //                        new AnnotationValues(oldValue: null, newValue: "UpdatedAt")
            //                    },
            //                }),
            //        Deleted = c.Boolean(nullable: false,
            //                annotations: new Dictionary<string, AnnotationValues>
            //                {
            //                    {
            //                        "ServiceTableColumn",
            //                        new AnnotationValues(oldValue: null, newValue: "Deleted")
            //                    },
            //                }),
            //    })
            //    .PrimaryKey(t => t.Id);
                //.Index(t => t.CreatedAt, clustered: true);

            CreateTable(
                "dbo.IChooseChartItems",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                {
                                    "ServiceTableColumn",
                                    new AnnotationValues(oldValue: null, newValue: "Id")
                                },
                            }),
                    IChooseChart = c.String(),
                    ItemText = c.String(),
                    ChartOption = c.Int(nullable: false),
                    ChartType = c.Int(nullable: false),
                    Archived = c.Boolean(nullable: false),
                    Migrated = c.Boolean(nullable: false),
                    UserID = c.String(),
                    Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion",
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                {
                                    "ServiceTableColumn",
                                    new AnnotationValues(oldValue: null, newValue: "Version")
                                },
                            }),
                    CreatedAt = c.DateTimeOffset(nullable: false, precision: 7,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                {
                                    "ServiceTableColumn",
                                    new AnnotationValues(oldValue: null, newValue: "CreatedAt")
                                },
                            }),
                    UpdatedAt = c.DateTimeOffset(precision: 7,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                {
                                    "ServiceTableColumn",
                                    new AnnotationValues(oldValue: null, newValue: "UpdatedAt")
                                },
                            }),
                    Deleted = c.Boolean(nullable: false,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                {
                                    "ServiceTableColumn",
                                    new AnnotationValues(oldValue: null, newValue: "Deleted")
                                },
                            }),
                })
                .PrimaryKey(t => t.Id);
                //.Index(t => t.CreatedAt, clustered: true);
            
            CreateTable(
                "dbo.ItemHighlights",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "ServiceTableColumn",
                                    new AnnotationValues(oldValue: null, newValue: "Id")
                                },
                            }),
                        ItemText = c.String(),
                        Italics = c.Boolean(nullable: false),
                        Bold = c.Boolean(nullable: false),
                        WithColour = c.Boolean(nullable: false),
                        IChooseChartItem = c.String(),
                        IChooseChart = c.String(),
                        FabicColour = c.Int(nullable: false),
                        ItemType = c.Int(nullable: false),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion",
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "ServiceTableColumn",
                                    new AnnotationValues(oldValue: null, newValue: "Version")
                                },
                            }),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "ServiceTableColumn",
                                    new AnnotationValues(oldValue: null, newValue: "CreatedAt")
                                },
                            }),
                        UpdatedAt = c.DateTimeOffset(precision: 7,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "ServiceTableColumn",
                                    new AnnotationValues(oldValue: null, newValue: "UpdatedAt")
                                },
                            }),
                        Deleted = c.Boolean(nullable: false,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "ServiceTableColumn",
                                    new AnnotationValues(oldValue: null, newValue: "Deleted")
                                },
                            }),
                        IChooseChartItem_Id = c.String(maxLength: 128),
                        IChooseChart_Id = c.String(maxLength: 128),
                        IChooseChart_Id1 = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IChooseChartItems", t => t.IChooseChartItem_Id)
                .ForeignKey("dbo.IChooseCharts", t => t.IChooseChart_Id)
                .ForeignKey("dbo.IChooseCharts", t => t.IChooseChart_Id1)
               // .Index(t => t.CreatedAt, clustered: true)
                .Index(t => t.IChooseChartItem_Id)
                .Index(t => t.IChooseChart_Id)
                .Index(t => t.IChooseChart_Id1);

            CreateTable(
                "dbo.IChooseCharts",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                {
                                    "ServiceTableColumn",
                                    new AnnotationValues(oldValue: null, newValue: "Id")
                                },
                            }),
                    Name = c.String(),
                    Description1 = c.String(),
                    Description2 = c.String(),
                    FabicExample = c.Boolean(nullable: false),
                    Archived = c.Boolean(nullable: false),
                    Migrated = c.Boolean(nullable: false),
                    UserID = c.String(),
                    Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion",
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                {
                                    "ServiceTableColumn",
                                    new AnnotationValues(oldValue: null, newValue: "Version")
                                },
                            }),
                    CreatedAt = c.DateTimeOffset(nullable: false, precision: 7,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                {
                                    "ServiceTableColumn",
                                    new AnnotationValues(oldValue: null, newValue: "CreatedAt")
                                },
                            }),
                    UpdatedAt = c.DateTimeOffset(precision: 7,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                {
                                    "ServiceTableColumn",
                                    new AnnotationValues(oldValue: null, newValue: "UpdatedAt")
                                },
                            }),
                    Deleted = c.Boolean(nullable: false,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                {
                                    "ServiceTableColumn",
                                    new AnnotationValues(oldValue: null, newValue: "Deleted")
                                },
                            }),
                })
                .PrimaryKey(t => t.Id);
                //.Index(t => t.CreatedAt, clustered: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ItemHighlights", "IChooseChart_Id1", "dbo.IChooseCharts");
            DropForeignKey("dbo.ItemHighlights", "IChooseChart_Id", "dbo.IChooseCharts");
            DropForeignKey("dbo.ItemHighlights", "IChooseChartItem_Id", "dbo.IChooseChartItems");
            DropIndex("dbo.IChooseCharts", new[] { "CreatedAt" });
            DropIndex("dbo.ItemHighlights", new[] { "IChooseChart_Id1" });
            DropIndex("dbo.ItemHighlights", new[] { "IChooseChart_Id" });
            DropIndex("dbo.ItemHighlights", new[] { "IChooseChartItem_Id" });
            DropIndex("dbo.ItemHighlights", new[] { "CreatedAt" });
            DropIndex("dbo.IChooseChartItems", new[] { "CreatedAt" });
            DropIndex("dbo.BehaviourScaleItems", new[] { "CreatedAt" });
            DropTable("dbo.IChooseCharts",
                removedColumnAnnotations: new Dictionary<string, IDictionary<string, object>>
                {
                    {
                        "CreatedAt",
                        new Dictionary<string, object>
                        {
                            { "ServiceTableColumn", "CreatedAt" },
                        }
                    },
                    {
                        "Deleted",
                        new Dictionary<string, object>
                        {
                            { "ServiceTableColumn", "Deleted" },
                        }
                    },
                    {
                        "Id",
                        new Dictionary<string, object>
                        {
                            { "ServiceTableColumn", "Id" },
                        }
                    },
                    {
                        "UpdatedAt",
                        new Dictionary<string, object>
                        {
                            { "ServiceTableColumn", "UpdatedAt" },
                        }
                    },
                    {
                        "Version",
                        new Dictionary<string, object>
                        {
                            { "ServiceTableColumn", "Version" },
                        }
                    },
                });
            DropTable("dbo.ItemHighlights",
                removedColumnAnnotations: new Dictionary<string, IDictionary<string, object>>
                {
                    {
                        "CreatedAt",
                        new Dictionary<string, object>
                        {
                            { "ServiceTableColumn", "CreatedAt" },
                        }
                    },
                    {
                        "Deleted",
                        new Dictionary<string, object>
                        {
                            { "ServiceTableColumn", "Deleted" },
                        }
                    },
                    {
                        "Id",
                        new Dictionary<string, object>
                        {
                            { "ServiceTableColumn", "Id" },
                        }
                    },
                    {
                        "UpdatedAt",
                        new Dictionary<string, object>
                        {
                            { "ServiceTableColumn", "UpdatedAt" },
                        }
                    },
                    {
                        "Version",
                        new Dictionary<string, object>
                        {
                            { "ServiceTableColumn", "Version" },
                        }
                    },
                });
            DropTable("dbo.IChooseChartItems",
                removedColumnAnnotations: new Dictionary<string, IDictionary<string, object>>
                {
                    {
                        "CreatedAt",
                        new Dictionary<string, object>
                        {
                            { "ServiceTableColumn", "CreatedAt" },
                        }
                    },
                    {
                        "Deleted",
                        new Dictionary<string, object>
                        {
                            { "ServiceTableColumn", "Deleted" },
                        }
                    },
                    {
                        "Id",
                        new Dictionary<string, object>
                        {
                            { "ServiceTableColumn", "Id" },
                        }
                    },
                    {
                        "UpdatedAt",
                        new Dictionary<string, object>
                        {
                            { "ServiceTableColumn", "UpdatedAt" },
                        }
                    },
                    {
                        "Version",
                        new Dictionary<string, object>
                        {
                            { "ServiceTableColumn", "Version" },
                        }
                    },
                });
            DropTable("dbo.BehaviourScaleItems",
                removedColumnAnnotations: new Dictionary<string, IDictionary<string, object>>
                {
                    {
                        "CreatedAt",
                        new Dictionary<string, object>
                        {
                            { "ServiceTableColumn", "CreatedAt" },
                        }
                    },
                    {
                        "Deleted",
                        new Dictionary<string, object>
                        {
                            { "ServiceTableColumn", "Deleted" },
                        }
                    },
                    {
                        "Id",
                        new Dictionary<string, object>
                        {
                            { "ServiceTableColumn", "Id" },
                        }
                    },
                    {
                        "UpdatedAt",
                        new Dictionary<string, object>
                        {
                            { "ServiceTableColumn", "UpdatedAt" },
                        }
                    },
                    {
                        "Version",
                        new Dictionary<string, object>
                        {
                            { "ServiceTableColumn", "Version" },
                        }
                    },
                });
        }
    }
}
