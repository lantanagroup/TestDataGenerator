<?xml version="1.0" encoding="UTF-8"?>
<structure version="12" xsltversion="1" htmlmode="strict" relativeto="*SPS" encodinghtml="UTF-16" encodingrtf="ISO-8859-1" encodingpdf="UTF-8" useimportschema="1" embed-images="1" enable-authentic-scripts="1" authentic-scripts-in-debug-mode-external="0" generated-file-location="DEFAULT">
	<parameters/>
	<schemasources>
		<namespaces/>
		<schemasources>
			<xsdschemasource name="XML" main="1" schemafile="MessageLog.xsd" workingxmlfile="bin\Debug\messages.xml"/>
		</schemasources>
	</schemasources>
	<modules/>
	<flags>
		<scripts/>
		<globalparts/>
		<designfragments/>
		<pagelayouts/>
	</flags>
	<scripts>
		<script language="javascript"/>
	</scripts>
	<script-project>
		<Project version="1" app="AuthenticView"/>
	</script-project>
	<importedxslt/>
	<globalstyles/>
	<mainparts>
		<children>
			<globaltemplate subtype="main" match="/">
				<children>
					<documentsection>
						<properties columncount="1" columngap="0.50in" headerfooterheight="fixed" pagemultiplepages="0" pagenumberingformat="1" pagenumberingstartat="auto" pagestart="next" paperheight="11in" papermarginbottom="0.79in" papermarginfooter="0.30in" papermarginheader="0.30in" papermarginleft="0.60in" papermarginright="0.60in" papermargintop="0.79in" paperwidth="8.50in"/>
					</documentsection>
					<template subtype="source" match="XML">
						<children>
							<newline/>
							<template subtype="element" match="root">
								<children>
									<newline/>
									<bookmark>
										<action>
											<none/>
										</action>
										<bookmark>
											<fixtext value="top"/>
										</bookmark>
									</bookmark>
									<newline/>
									<list>
										<properties start="0"/>
										<children>
											<listrow>
												<children>
													<link>
														<children>
															<text fixtext="Errors ("/>
															<autocalc xpath="count(message[@severity=&apos;Error&apos;])"/>
															<text fixtext=")"/>
														</children>
														<action>
															<none/>
														</action>
														<hyperlink>
															<fixtext value="#errors"/>
														</hyperlink>
													</link>
												</children>
											</listrow>
											<listrow>
												<children>
													<link>
														<children>
															<text fixtext="Warnings ("/>
															<autocalc xpath="count(message[@severity=&apos;Warning&apos;])"/>
															<text fixtext=")"/>
														</children>
														<action>
															<none/>
														</action>
														<hyperlink>
															<fixtext value="#warnings"/>
														</hyperlink>
													</link>
												</children>
											</listrow>
											<listrow>
												<children>
													<link>
														<children>
															<text fixtext="Information ("/>
															<autocalc xpath="count(message[@severity=&apos;Information&apos;])"/>
															<text fixtext=")"/>
														</children>
														<action>
															<none/>
														</action>
														<hyperlink>
															<fixtext value="#information"/>
														</hyperlink>
													</link>
												</children>
											</listrow>
											<listrow>
												<children>
													<link>
														<children>
															<text fixtext="Schema Validation ("/>
															<autocalc xpath="count(message[@type=&apos;Schema Validation&apos;])"/>
															<text fixtext=")"/>
														</children>
														<action>
															<none/>
														</action>
														<hyperlink>
															<fixtext value="#schema_validation"/>
														</hyperlink>
													</link>
												</children>
											</listrow>
											<listrow>
												<children>
													<link>
														<children>
															<text fixtext="Content Validation ("/>
															<autocalc xpath="count(message[@type=&apos;Content Validation&apos;])"/>
															<text fixtext=")"/>
														</children>
														<action>
															<none/>
														</action>
														<hyperlink>
															<fixtext value="#content_validation"/>
														</hyperlink>
													</link>
												</children>
											</listrow>
											<listrow>
												<children>
													<link>
														<children>
															<text fixtext="Generation ("/>
															<autocalc xpath="count(message[@type=&apos;Generation&apos;])"/>
															<text fixtext=")"/>
														</children>
														<action>
															<none/>
														</action>
														<hyperlink>
															<fixtext value="#generation"/>
														</hyperlink>
													</link>
												</children>
											</listrow>
											<listrow>
												<children>
													<link>
														<children>
															<text fixtext="All ("/>
															<autocalc xpath="count(message)"/>
															<text fixtext=")"/>
														</children>
														<action>
															<none/>
														</action>
														<hyperlink>
															<fixtext value="#all"/>
														</hyperlink>
													</link>
												</children>
											</listrow>
										</children>
									</list>
									<newline/>
									<paragraph paragraphtag="h1">
										<children>
											<text fixtext="Message Log ("/>
											<template subtype="attribute" match="date">
												<children>
													<content/>
												</children>
												<variables/>
											</template>
											<text fixtext=")"/>
										</children>
									</paragraph>
									<paragraph paragraphtag="h2">
										<children>
											<bookmark>
												<children>
													<text fixtext="Severity: Errors"/>
												</children>
												<action>
													<none/>
												</action>
												<bookmark>
													<fixtext value="errors"/>
												</bookmark>
											</bookmark>
										</children>
									</paragraph>
									<tgrid>
										<properties border="1"/>
										<children>
											<tgridbody-cols>
												<children>
													<tgridcol/>
													<tgridcol/>
													<tgridcol/>
													<tgridcol/>
												</children>
											</tgridbody-cols>
											<tgridheader-rows>
												<children>
													<tgridrow>
														<children>
															<tgridcell>
																<children>
																	<text fixtext="type"/>
																</children>
															</tgridcell>
															<tgridcell>
																<children>
																	<text fixtext="time"/>
																</children>
															</tgridcell>
															<tgridcell>
																<children>
																	<text fixtext="message"/>
																</children>
															</tgridcell>
															<tgridcell>
																<children>
																	<text fixtext="file"/>
																</children>
															</tgridcell>
														</children>
													</tgridrow>
												</children>
											</tgridheader-rows>
											<tgridbody-rows>
												<children>
													<template subtype="element" filter="@severity=&apos;Error&apos;" match="message">
														<children>
															<tgridrow>
																<children>
																	<tgridcell>
																		<children>
																			<template subtype="attribute" match="type">
																				<children>
																					<content/>
																				</children>
																				<variables/>
																			</template>
																		</children>
																	</tgridcell>
																	<tgridcell>
																		<children>
																			<template subtype="attribute" match="time">
																				<children>
																					<content>
																						<format basic-type="xsd" datatype="time"/>
																					</content>
																				</children>
																				<variables/>
																			</template>
																		</children>
																	</tgridcell>
																	<tgridcell>
																		<children>
																			<template subtype="attribute" match="message">
																				<children>
																					<content/>
																				</children>
																				<variables/>
																			</template>
																		</children>
																	</tgridcell>
																	<tgridcell>
																		<children>
																			<template subtype="attribute" match="file">
																				<children>
																					<content/>
																				</children>
																				<variables/>
																			</template>
																		</children>
																	</tgridcell>
																</children>
															</tgridrow>
														</children>
														<variables/>
													</template>
												</children>
											</tgridbody-rows>
										</children>
									</tgrid>
									<link>
										<children>
											<text fixtext="Top"/>
										</children>
										<action>
											<none/>
										</action>
										<hyperlink>
											<fixtext value="#top"/>
										</hyperlink>
									</link>
									<newline/>
									<paragraph paragraphtag="h2">
										<children>
											<bookmark>
												<children>
													<text fixtext="Severity: Warnings"/>
												</children>
												<action>
													<none/>
												</action>
												<bookmark>
													<fixtext value="warnings"/>
												</bookmark>
											</bookmark>
										</children>
									</paragraph>
									<tgrid>
										<properties border="1"/>
										<children>
											<tgridbody-cols>
												<children>
													<tgridcol/>
													<tgridcol/>
													<tgridcol/>
													<tgridcol/>
												</children>
											</tgridbody-cols>
											<tgridheader-rows>
												<children>
													<tgridrow>
														<children>
															<tgridcell>
																<children>
																	<text fixtext="type"/>
																</children>
															</tgridcell>
															<tgridcell>
																<children>
																	<text fixtext="time"/>
																</children>
															</tgridcell>
															<tgridcell>
																<children>
																	<text fixtext="message"/>
																</children>
															</tgridcell>
															<tgridcell>
																<children>
																	<text fixtext="file"/>
																</children>
															</tgridcell>
														</children>
													</tgridrow>
												</children>
											</tgridheader-rows>
											<tgridbody-rows>
												<children>
													<template subtype="element" filter="@severity=&apos;Warning&apos;" match="message">
														<children>
															<tgridrow>
																<children>
																	<tgridcell>
																		<children>
																			<template subtype="attribute" match="type">
																				<children>
																					<content/>
																				</children>
																				<variables/>
																			</template>
																		</children>
																	</tgridcell>
																	<tgridcell>
																		<children>
																			<template subtype="attribute" match="time">
																				<children>
																					<content>
																						<format basic-type="xsd" datatype="time"/>
																					</content>
																				</children>
																				<variables/>
																			</template>
																		</children>
																	</tgridcell>
																	<tgridcell>
																		<children>
																			<template subtype="attribute" match="message">
																				<children>
																					<content/>
																				</children>
																				<variables/>
																			</template>
																		</children>
																	</tgridcell>
																	<tgridcell>
																		<children>
																			<template subtype="attribute" match="file">
																				<children>
																					<content/>
																				</children>
																				<variables/>
																			</template>
																		</children>
																	</tgridcell>
																</children>
															</tgridrow>
														</children>
														<variables/>
													</template>
												</children>
											</tgridbody-rows>
										</children>
									</tgrid>
									<link>
										<children>
											<text fixtext="Top"/>
										</children>
										<action>
											<none/>
										</action>
										<hyperlink>
											<fixtext value="#top"/>
										</hyperlink>
									</link>
									<newline/>
									<paragraph paragraphtag="h2">
										<children>
											<bookmark>
												<children>
													<text fixtext="Severity: Information"/>
												</children>
												<action>
													<none/>
												</action>
												<bookmark>
													<fixtext value="information"/>
												</bookmark>
											</bookmark>
										</children>
									</paragraph>
									<tgrid>
										<properties border="1"/>
										<children>
											<tgridbody-cols>
												<children>
													<tgridcol/>
													<tgridcol/>
													<tgridcol/>
													<tgridcol/>
												</children>
											</tgridbody-cols>
											<tgridheader-rows>
												<children>
													<tgridrow>
														<children>
															<tgridcell>
																<children>
																	<text fixtext="type"/>
																</children>
															</tgridcell>
															<tgridcell>
																<children>
																	<text fixtext="time"/>
																</children>
															</tgridcell>
															<tgridcell>
																<children>
																	<text fixtext="message"/>
																</children>
															</tgridcell>
															<tgridcell>
																<children>
																	<text fixtext="file"/>
																</children>
															</tgridcell>
														</children>
													</tgridrow>
												</children>
											</tgridheader-rows>
											<tgridbody-rows>
												<children>
													<template subtype="element" filter="@severity=&apos;Information&apos;" match="message">
														<children>
															<tgridrow>
																<children>
																	<tgridcell>
																		<children>
																			<template subtype="attribute" match="type">
																				<children>
																					<content/>
																				</children>
																				<variables/>
																			</template>
																		</children>
																	</tgridcell>
																	<tgridcell>
																		<children>
																			<template subtype="attribute" match="time">
																				<children>
																					<content>
																						<format basic-type="xsd" datatype="time"/>
																					</content>
																				</children>
																				<variables/>
																			</template>
																		</children>
																	</tgridcell>
																	<tgridcell>
																		<children>
																			<template subtype="attribute" match="message">
																				<children>
																					<content/>
																				</children>
																				<variables/>
																			</template>
																		</children>
																	</tgridcell>
																	<tgridcell>
																		<children>
																			<template subtype="attribute" match="file">
																				<children>
																					<content/>
																				</children>
																				<variables/>
																			</template>
																		</children>
																	</tgridcell>
																</children>
															</tgridrow>
														</children>
														<variables/>
													</template>
												</children>
											</tgridbody-rows>
										</children>
									</tgrid>
									<link>
										<children>
											<text fixtext="Top"/>
										</children>
										<action>
											<none/>
										</action>
										<hyperlink>
											<fixtext value="#top"/>
										</hyperlink>
									</link>
									<newline/>
									<paragraph paragraphtag="h2">
										<children>
											<bookmark>
												<children>
													<text fixtext="Type: Schema Validation"/>
												</children>
												<action>
													<none/>
												</action>
												<bookmark>
													<fixtext value="schema_validation"/>
												</bookmark>
											</bookmark>
										</children>
									</paragraph>
									<tgrid>
										<properties border="1"/>
										<children>
											<tgridbody-cols>
												<children>
													<tgridcol/>
													<tgridcol/>
													<tgridcol/>
													<tgridcol/>
													<tgridcol/>
													<tgridcol/>
												</children>
											</tgridbody-cols>
											<tgridheader-rows>
												<children>
													<tgridrow>
														<children>
															<tgridcell>
																<children>
																	<text fixtext="severity"/>
																</children>
															</tgridcell>
															<tgridcell>
																<children>
																	<text fixtext="time"/>
																</children>
															</tgridcell>
															<tgridcell>
																<children>
																	<text fixtext="message"/>
																</children>
															</tgridcell>
															<tgridcell>
																<children>
																	<text fixtext="file"/>
																</children>
															</tgridcell>
															<tgridcell>
																<children>
																	<text fixtext="line"/>
																</children>
															</tgridcell>
															<tgridcell>
																<children>
																	<text fixtext="position"/>
																</children>
															</tgridcell>
														</children>
													</tgridrow>
												</children>
											</tgridheader-rows>
											<tgridbody-rows>
												<children>
													<template subtype="element" filter="@type=&apos;Schema Validation&apos;" match="message">
														<children>
															<tgridrow>
																<children>
																	<tgridcell>
																		<children>
																			<template subtype="attribute" match="severity">
																				<children>
																					<content/>
																				</children>
																				<variables/>
																			</template>
																		</children>
																	</tgridcell>
																	<tgridcell>
																		<children>
																			<template subtype="attribute" match="time">
																				<children>
																					<content>
																						<format basic-type="xsd" datatype="time"/>
																					</content>
																				</children>
																				<variables/>
																			</template>
																		</children>
																	</tgridcell>
																	<tgridcell>
																		<children>
																			<template subtype="attribute" match="message">
																				<children>
																					<content/>
																				</children>
																				<variables/>
																			</template>
																		</children>
																	</tgridcell>
																	<tgridcell>
																		<children>
																			<template subtype="attribute" match="file">
																				<children>
																					<content/>
																				</children>
																				<variables/>
																			</template>
																		</children>
																	</tgridcell>
																	<tgridcell>
																		<children>
																			<template subtype="element" match="location">
																				<children>
																					<template subtype="attribute" match="line">
																						<children>
																							<content/>
																						</children>
																						<variables/>
																					</template>
																				</children>
																				<variables/>
																			</template>
																		</children>
																	</tgridcell>
																	<tgridcell>
																		<children>
																			<template subtype="element" match="location">
																				<children>
																					<template subtype="attribute" match="position">
																						<children>
																							<content/>
																						</children>
																						<variables/>
																					</template>
																				</children>
																				<variables/>
																			</template>
																		</children>
																	</tgridcell>
																</children>
															</tgridrow>
														</children>
														<variables/>
													</template>
												</children>
											</tgridbody-rows>
										</children>
									</tgrid>
									<link>
										<children>
											<text fixtext="Top"/>
										</children>
										<action>
											<none/>
										</action>
										<hyperlink>
											<fixtext value="#top"/>
										</hyperlink>
									</link>
									<newline/>
									<paragraph paragraphtag="h2">
										<children>
											<bookmark>
												<children>
													<text fixtext="Type: Content Validation"/>
												</children>
												<action>
													<none/>
												</action>
												<bookmark>
													<fixtext value="content_validation"/>
												</bookmark>
											</bookmark>
										</children>
									</paragraph>
									<tgrid>
										<properties border="1"/>
										<children>
											<tgridbody-cols>
												<children>
													<tgridcol/>
													<tgridcol/>
													<tgridcol/>
													<tgridcol/>
													<tgridcol/>
													<tgridcol/>
												</children>
											</tgridbody-cols>
											<tgridheader-rows>
												<children>
													<tgridrow>
														<children>
															<tgridcell>
																<children>
																	<text fixtext="severity"/>
																</children>
															</tgridcell>
															<tgridcell>
																<children>
																	<text fixtext="time"/>
																</children>
															</tgridcell>
															<tgridcell>
																<children>
																	<text fixtext="message"/>
																</children>
															</tgridcell>
															<tgridcell>
																<children>
																	<text fixtext="file"/>
																</children>
															</tgridcell>
															<tgridcell>
																<children>
																	<text fixtext="context"/>
																</children>
															</tgridcell>
															<tgridcell>
																<children>
																	<text fixtext="test"/>
																</children>
															</tgridcell>
														</children>
													</tgridrow>
												</children>
											</tgridheader-rows>
											<tgridbody-rows>
												<children>
													<template subtype="element" filter="@type=&apos;Content Validation&apos;" match="message">
														<children>
															<tgridrow>
																<children>
																	<tgridcell>
																		<children>
																			<template subtype="attribute" match="severity">
																				<children>
																					<content/>
																				</children>
																				<variables/>
																			</template>
																		</children>
																	</tgridcell>
																	<tgridcell>
																		<children>
																			<template subtype="attribute" match="time">
																				<children>
																					<content>
																						<format basic-type="xsd" datatype="time"/>
																					</content>
																				</children>
																				<variables/>
																			</template>
																		</children>
																	</tgridcell>
																	<tgridcell>
																		<children>
																			<template subtype="attribute" match="message">
																				<children>
																					<content/>
																				</children>
																				<variables/>
																			</template>
																		</children>
																	</tgridcell>
																	<tgridcell>
																		<children>
																			<template subtype="attribute" match="file">
																				<children>
																					<content/>
																				</children>
																				<variables/>
																			</template>
																		</children>
																	</tgridcell>
																	<tgridcell>
																		<children>
																			<template subtype="element" match="context">
																				<children>
																					<content/>
																				</children>
																				<variables/>
																			</template>
																		</children>
																	</tgridcell>
																	<tgridcell>
																		<children>
																			<template subtype="element" match="test">
																				<children>
																					<content/>
																				</children>
																				<variables/>
																			</template>
																		</children>
																	</tgridcell>
																</children>
															</tgridrow>
														</children>
														<variables/>
													</template>
												</children>
											</tgridbody-rows>
										</children>
									</tgrid>
									<link>
										<children>
											<text fixtext="Top"/>
										</children>
										<action>
											<none/>
										</action>
										<hyperlink>
											<fixtext value="#top"/>
										</hyperlink>
									</link>
									<newline/>
									<paragraph paragraphtag="h2">
										<children>
											<bookmark>
												<children>
													<text fixtext="Type: Generation"/>
												</children>
												<action>
													<none/>
												</action>
												<bookmark>
													<fixtext value="generation"/>
												</bookmark>
											</bookmark>
										</children>
									</paragraph>
									<tgrid>
										<properties border="1"/>
										<children>
											<tgridbody-cols>
												<children>
													<tgridcol/>
													<tgridcol/>
													<tgridcol/>
													<tgridcol/>
												</children>
											</tgridbody-cols>
											<tgridheader-rows>
												<children>
													<tgridrow>
														<children>
															<tgridcell>
																<children>
																	<text fixtext="severity"/>
																</children>
															</tgridcell>
															<tgridcell>
																<children>
																	<text fixtext="time"/>
																</children>
															</tgridcell>
															<tgridcell>
																<children>
																	<text fixtext="message"/>
																</children>
															</tgridcell>
															<tgridcell>
																<children>
																	<text fixtext="file"/>
																</children>
															</tgridcell>
														</children>
													</tgridrow>
												</children>
											</tgridheader-rows>
											<tgridbody-rows>
												<children>
													<template subtype="element" filter="@type=&apos;Generation&apos;" match="message">
														<children>
															<tgridrow>
																<children>
																	<tgridcell>
																		<children>
																			<template subtype="attribute" match="severity">
																				<children>
																					<content/>
																				</children>
																				<variables/>
																			</template>
																		</children>
																	</tgridcell>
																	<tgridcell>
																		<children>
																			<template subtype="attribute" match="time">
																				<children>
																					<content>
																						<format basic-type="xsd" datatype="time"/>
																					</content>
																				</children>
																				<variables/>
																			</template>
																		</children>
																	</tgridcell>
																	<tgridcell>
																		<children>
																			<template subtype="attribute" match="message">
																				<children>
																					<content/>
																				</children>
																				<variables/>
																			</template>
																		</children>
																	</tgridcell>
																	<tgridcell>
																		<children>
																			<template subtype="attribute" match="file">
																				<children>
																					<content/>
																				</children>
																				<variables/>
																			</template>
																		</children>
																	</tgridcell>
																</children>
															</tgridrow>
														</children>
														<variables/>
													</template>
												</children>
											</tgridbody-rows>
										</children>
									</tgrid>
									<link>
										<children>
											<text fixtext="Top"/>
										</children>
										<action>
											<none/>
										</action>
										<hyperlink>
											<fixtext value="#top"/>
										</hyperlink>
									</link>
									<newline/>
									<paragraph paragraphtag="h2">
										<children>
											<bookmark>
												<children>
													<text fixtext="All"/>
												</children>
												<action>
													<none/>
												</action>
												<bookmark>
													<fixtext value="all"/>
												</bookmark>
											</bookmark>
										</children>
									</paragraph>
									<tgrid>
										<properties border="1"/>
										<children>
											<tgridbody-cols>
												<children>
													<tgridcol/>
													<tgridcol/>
													<tgridcol/>
													<tgridcol/>
													<tgridcol/>
												</children>
											</tgridbody-cols>
											<tgridheader-rows>
												<children>
													<tgridrow>
														<children>
															<tgridcell>
																<children>
																	<text fixtext="severity"/>
																</children>
															</tgridcell>
															<tgridcell>
																<children>
																	<text fixtext="type"/>
																</children>
															</tgridcell>
															<tgridcell>
																<children>
																	<text fixtext="time"/>
																</children>
															</tgridcell>
															<tgridcell>
																<children>
																	<text fixtext="message"/>
																</children>
															</tgridcell>
															<tgridcell>
																<children>
																	<text fixtext="file"/>
																</children>
															</tgridcell>
														</children>
													</tgridrow>
												</children>
											</tgridheader-rows>
											<tgridbody-rows>
												<children>
													<template subtype="element" match="message">
														<children>
															<tgridrow>
																<styles height="0.20in"/>
																<children>
																	<tgridcell>
																		<children>
																			<template subtype="attribute" match="severity">
																				<children>
																					<content/>
																				</children>
																				<variables/>
																			</template>
																		</children>
																	</tgridcell>
																	<tgridcell>
																		<children>
																			<template subtype="attribute" match="type">
																				<children>
																					<content/>
																				</children>
																				<variables/>
																			</template>
																		</children>
																	</tgridcell>
																	<tgridcell>
																		<children>
																			<template subtype="attribute" match="time">
																				<children>
																					<content>
																						<format basic-type="xsd" datatype="time"/>
																					</content>
																				</children>
																				<variables/>
																			</template>
																		</children>
																	</tgridcell>
																	<tgridcell>
																		<children>
																			<template subtype="attribute" match="message">
																				<children>
																					<content/>
																				</children>
																				<variables/>
																			</template>
																		</children>
																	</tgridcell>
																	<tgridcell>
																		<children>
																			<template subtype="attribute" match="file">
																				<children>
																					<content/>
																				</children>
																				<variables/>
																			</template>
																		</children>
																	</tgridcell>
																</children>
															</tgridrow>
														</children>
														<variables/>
													</template>
												</children>
											</tgridbody-rows>
										</children>
									</tgrid>
									<link>
										<children>
											<text fixtext="Top"/>
										</children>
										<action>
											<none/>
										</action>
										<hyperlink>
											<fixtext value="#top"/>
										</hyperlink>
									</link>
									<newline/>
								</children>
								<variables/>
							</template>
							<newline/>
						</children>
						<variables/>
					</template>
				</children>
			</globaltemplate>
		</children>
	</mainparts>
	<globalparts/>
	<pagelayout>
		<properties paperheight="11.69in" paperwidth="8.27in"/>
	</pagelayout>
	<designfragments/>
	<xmltables/>
	<authentic-custom-toolbar-buttons/>
</structure>
